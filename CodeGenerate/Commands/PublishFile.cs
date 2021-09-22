using CodeGenerate.BusinessCode;
using EnvDTE;
using EnvDTE80;
using Microsoft.VisualStudio.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Task = System.Threading.Tasks.Task;

namespace CodeGenerate
{
    /// <summary>
    /// Command handler
    /// </summary>
    internal sealed class PublishFile
    {
        /// <summary>
        /// Command ID.
        /// </summary>
        public const int CommandId = 0x0101;

        /// <summary>
        /// Command menu group (command set GUID).
        /// </summary>
        public static readonly Guid CommandSet = new Guid("b4c506cb-89dc-4922-addf-3cefe8fbbc63");

        /// <summary>
        /// VS Package that provides this command, not null.
        /// </summary>
        private readonly AsyncPackage package;

        /// <summary>
        /// Initializes a new instance of the <see cref="PublishFile"/> class.
        /// Adds our command handlers for menu (commands must exist in the command table file)
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        /// <param name="commandService">Command service to add command to, not null.</param>
        private PublishFile(AsyncPackage package, OleMenuCommandService commandService)
        {
            this.package = package ?? throw new ArgumentNullException(nameof(package));
            commandService = commandService ?? throw new ArgumentNullException(nameof(commandService));

            var menuCommandID = new CommandID(CommandSet, CommandId);
            var menuItem = new MenuCommand(this.Execute, menuCommandID);
            commandService.AddCommand(menuItem);
        }

        /// <summary>
        /// Gets the instance of the command.
        /// </summary>
        public static PublishFile Instance
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the service provider from the owner package.
        /// </summary>
        private Microsoft.VisualStudio.Shell.IAsyncServiceProvider ServiceProvider
        {
            get
            {
                return this.package;
            }
        }

        /// <summary>
        /// Initializes the singleton instance of the command.
        /// </summary>
        /// <param name="package">Owner package, not null.</param>
        public static async Task InitializeAsync(AsyncPackage package)
        {
            // Verify the current thread is the UI thread - the call to AddCommand in PublishFile's constructor requires
            // the UI thread.
            await ThreadHelper.JoinableTaskFactory.SwitchToMainThreadAsync(package.DisposalToken);

            OleMenuCommandService commandService = await package.GetServiceAsync((typeof(IMenuCommandService))) as OleMenuCommandService;
            Instance = new PublishFile(package, commandService);
        }

        /// <summary>
        /// This function is the callback used to execute the command when the menu item is clicked.
        /// See the constructor to see how the menu item is associated with this function using
        /// OleMenuCommandService service and MenuCommand class.
        /// </summary>
        /// <param name="sender">Event sender.</param>
        /// <param name="e">Event args.</param>
        private void Execute(object sender, EventArgs e)
        {
            //var button = (MenuCommand)sender;
            // 右键点击后选中的文件(一般是一个,但也可以多个);当前项目只能同时生成一个
            var items = ProjectHelpers.GetSelectedItemPaths();
            if (items.Count() == 0)
            {
                MessageBox.Show("请选择至少一个文件");
                return;
            }
            // 当前活动项目路径
            Project activeProj = ProjectHelpers.GetActiveProject();
            if (activeProj == null)
            {
                MessageBox.Show("获取路径失败");
                return;
            }
            // 取得要发布的文件路径
            List<string> srcfiles = new List<string>();
            foreach (string path in items)
            {
                // 可能选中的是目录
                if (File.Exists(path))
                    srcfiles.Add(path);
            }
            if (srcfiles.Count == 0)
            {
                MessageBox.Show("获取地址失败");
                return;
            }

            ModelAnalysis modelAnalysis = new ModelAnalysis();
            string errorMessage = "";
            if (!modelAnalysis.ReadFile(srcfiles.FirstOrDefault(), ref errorMessage))
            {
                MessageBox.Show(errorMessage);
                return;
            }
            CodeMain codeMain = new CodeMain();
            codeMain.Show();
        }
    }
}

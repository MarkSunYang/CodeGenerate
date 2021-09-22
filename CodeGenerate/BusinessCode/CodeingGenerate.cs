using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeGenerate.BusinessCode
{
    public class CodeingGenerate
    {
        private static string CompanyName = "CompanyName";
        /// <summary>
        /// Map文件模板
        /// </summary>
        private string mapTxt = $"using {CompanyName}.Datas.Ef.SqlServer;\r\n" +
            "using {0};\r\n" +
            "using Microsoft.EntityFrameworkCore;\r\n" +
            "using Microsoft.EntityFrameworkCore.Metadata.Builders;\r\n" +
            "namespace {1}.Infrastructure.EntityConfigurations\r\n" +
            "{\r\n    " +
            "# <summary>\r\n    " +
            "# {5}Map\r\n   " +
            " # <!summary>\r\n   " +
            " public class {2}Map : " +
            "{3}<{2}>\r\n    " +
            "{\r\n        " +
            "#<summary>\r\n       " +
            " # 映射表\r\n        " +
            "# <!summary>\r\n        " +
            "# <param name=^builder^><!param>\r\n        " +
            "protected override void MapTable(EntityTypeBuilder<{2}> builder)\r\n        " +
            "{\r\n            " +
            "builder.ToTable(^{4}^);\r\n        " +
            "}\r\n";

        private string CreateTxt = $"\r\nusing {CompanyName}.Util.Applications.Dtos;\r\n" +
            "using System.ComponentModel.DataAnnotations;\r\n" +
            "using System.Collections.Generic;{0}\r\n" +
            "namespace {1}.Dtos\r\n" +
            "{\r\n    " +
            "# <summary>\r\n    " +
            "# {2}{5}请求Dto\r\n    " +
            "# <!summary>\r\n    " +
            "public class {3}{4}Dto : {6}\r\n" +
            "    {";

        /// <summary>
        /// Map文件生成规则
        /// </summary>
        public void CodeingMap()
        {
            string filePath = ModelAnalysis._Model.FilePath;
            string path = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\Infrastructure\\EntityConfigurations");
            string path2 = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\Infrastructure\\EntityConfigurations\\" + ModelAnalysis._Model.ModelName + "Map.cs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string text = "";
            IEnumerable<ModelField> enumerable = ModelAnalysis._Model.ModelField.Where((ModelField x) => x.IsRequired || (x.MaximumValue > 0.0 && x.AttrDataType == "string"));
            if (enumerable.Any())
            {
                text = text + "\r\n        # <summary>\r\n        # 属性映射\r\n        # <!summary>\r\n        # <param name=^builder^><!param>\r\n        protected override void MapProperties(EntityTypeBuilder<" + ModelAnalysis._Model.ModelName + "> builder)\r\n        {";
                foreach (ModelField item in enumerable)
                {
                    text = text + "\r\n            builder.Property(c => c." + item.AttrName + ")";
                    if (item.IsRequired)
                    {
                        text += ".IsRequired()";
                    }
                    if (item.MaximumValue > 0.0 && item.AttrDataType == "string")
                    {
                        text = text + ".HasMaxLength(" + item.MaximumValue + ")";
                    }
                    text += ";";
                }
                text += "\r\n        }";
            }
            string newValue = "EntityMap";
            if (ModelAnalysis._Model.BaseEntities.IndexOf("AggregateRoot") > -1)
            {
                newValue = "AggregateRootMap";
            }
            string text2 = ModelAnalysis._Model.TableName;
            if (!string.IsNullOrWhiteSpace(ModelAnalysis._Model.Prefix))
            {
                text2 = ModelAnalysis._Model.Prefix + "_" + text2;
            }
            string text3 = mapTxt.Replace("{0}", ModelAnalysis._Model.BaseNamespace).Replace("{1}", ModelAnalysis._Model.Namespace).Replace("{2}", ModelAnalysis._Model.ModelName)
                .Replace("{3}", newValue)
                .Replace("{4}", text2)
                .Replace("{5}", ModelAnalysis._Model.ModelDesc);
            text3 = text3 + text + "\r\n    }\r\n}";
            FileStream fileStream = new FileStream(path2, FileMode.Create);
            byte[] bytes = Encoding.UTF8.GetBytes(text3.Replace("#", "///").Replace("!", "/").Replace("^", "\""));
            fileStream.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// 创建
        /// </summary>
        public void CodeingCreate()
        {
            string filePath = ModelAnalysis._Model.FilePath;
            string path = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\Dtos\\" + ModelAnalysis._Model.ModelName);
            string path2 = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\Dtos\\" + ModelAnalysis._Model.ModelName + "\\" + ModelAnalysis._Model.ModelName + "CreateDto.cs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            CodeingCreateOrUpdate(path2, "Create");
        }

        /// <summary>
        /// 更新
        /// </summary>
        public void CodeingUpdate()
        {
            string filePath = ModelAnalysis._Model.FilePath;
            string path = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\Dtos\\" + ModelAnalysis._Model.ModelName);
            string path2 = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\Dtos\\" + ModelAnalysis._Model.ModelName + "\\" + ModelAnalysis._Model.ModelName + "UpdateDto.cs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            CodeingCreateOrUpdate(path2, "Update");
        }

        /// <summary>
        /// 新建或更新
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        private void CodeingCreateOrUpdate(string path, string name)
        {
            IEnumerable<ModelField> enumerable = ((name == "Create") ? ModelAnalysis._Model.ModelField.Where((ModelField x) => x.IsCreateRequest) : ModelAnalysis._Model.ModelField.Where((ModelField x) => x.IsUpdateRequest));
            string text = "";
            foreach (ModelField item in enumerable)
            {
                text = text + "\r\n        # <summary>\r\n        # " + item.AttrDesc + "\r\n        # <!summary>\r\n";
                if (item.IsRequired)
                {
                    text = text + "        [Required(ErrorMessage = ^" + item.AttrDesc + "是必填^)]\r\n";
                }
                if ((item.MaximumValue > 0.0 || item.MinimumValue > 0.0) && item.AttrDataType == "string")
                {
                    text = text + "        [StringLength({0}, ErrorMessage = ^" + item.AttrDesc;
                    if (item.MaximumValue > 0.0)
                    {
                        text = text.Replace("{0}", item.MaximumValue?.ToString());
                        text = text + ",不能超过" + item.MaximumValue + "位{0}";
                    }
                    else
                    {
                        text = text.Replace("{0}", "int.MaxValue");
                        text += "{0}";
                    }
                    if (item.MinimumValue > 0.0)
                    {
                        text = text.Replace("{0}", ",不能小于" + item.MinimumValue + "位^");
                        text = text + ", MinimumLength = " + item.MinimumValue;
                    }
                    else
                    {
                        text = text.Replace("{0}", "^");
                    }
                    text += ")]\r\n";
                }
                if ((item.MinimumValue.HasValue || item.MinimumValue.HasValue) && (item.AttrDataType.IndexOf("int") > -1 || item.AttrDataType.IndexOf("decimal") > -1 || item.AttrDataType.IndexOf("double") > -1 || item.AttrDataType.IndexOf("float") > -1 || item.AttrDataType.IndexOf("long") > -1))
                {
                    text = text + "        [Range(maximum:{0},minimum:{1}, ErrorMessage = ^" + item.AttrDesc;
                    if (item.MaximumValue.HasValue)
                    {
                        text = text.Replace("{0}", item.MaximumValue?.ToString());
                        text = text + ",不能大于" + item.MaximumValue + "{0}";
                    }
                    else
                    {
                        text = text.Replace("{0}", "double.MaxValue");
                        text += "{0}";
                    }
                    if (item.MinimumValue.HasValue)
                    {
                        text = text.Replace("{0}", ",不能小于" + item.MinimumValue + "^");
                        text = text.Replace("{1}", item.MinimumValue?.ToString());
                    }
                    else
                    {
                        text = text.Replace("{0}", "^");
                        text = text.Replace("{1}", "double.MinValue");
                    }
                    text += ")]\r\n";
                }
                text = text + "        public " + item.AttrDataType + " " + item.AttrName + " { get; set; }\r\n";
            }
            text += "\r\n    }\r\n}";
            string createTxt = CreateTxt;
            createTxt = ((!string.IsNullOrWhiteSpace(ModelAnalysis._Model.UsingEnum)) ? createTxt.Replace("{0}", "") : createTxt.Replace("{0}", "\r\n" + ModelAnalysis._Model.UsingEnum));
            createTxt = ((!(name == "Update")) ? createTxt.Replace("{6}", "RequestBase") : createTxt.Replace("{6}", "UpdateRequestBase"));
            createTxt = createTxt.Replace("{1}", ModelAnalysis._Model.Namespace).Replace("{2}", ModelAnalysis._Model.ModelDesc).Replace("{3}", ModelAnalysis._Model.ModelName)
                .Replace("{4}", name)
                .Replace("{5}", (name == "Create") ? "创建" : "修改");
            text = createTxt + text;
            FileStream fileStream = new FileStream(path, FileMode.Create);
            byte[] bytes = Encoding.UTF8.GetBytes(text.Replace("#", "///").Replace("!", "/").Replace("^", "\""));
            fileStream.Write(bytes, 0, bytes.Length);
        }

        public void CodeingQuery()
        {
            string filePath = ModelAnalysis._Model.FilePath;
            string path = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\Dtos\\" + ModelAnalysis._Model.ModelName);
            string path2 = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\Dtos\\" + ModelAnalysis._Model.ModelName + "\\" + ModelAnalysis._Model.ModelName + "QueryDto.cs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string text = $"using {CompanyName}.Util.Datas.Queries;\r\nnamespace " + ModelAnalysis._Model.Namespace + ".Dtos\r\n~\r\n    # <summary>\r\n    # " + ModelAnalysis._Model.ModelDesc + "查询Dto\r\n    # <!summary>\r\n    public class " + ModelAnalysis._Model.ModelName + "QueryDto : QueryParameter\r\n    ~\r\n    `\r\n`";
            FileStream fileStream = new FileStream(path2, FileMode.Create);
            byte[] bytes = Encoding.UTF8.GetBytes(text.Replace("#", "///").Replace("!", "/").Replace("^", "\"")
                .Replace("~", "{")
                .Replace("`", "}"));
            fileStream.Write(bytes, 0, bytes.Length);
        }

        public void CodeingResponse()
        {
            string filePath = ModelAnalysis._Model.FilePath;
            string path = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\Dtos\\" + ModelAnalysis._Model.ModelName);
            string path2 = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\Dtos\\" + ModelAnalysis._Model.ModelName + "\\" + ModelAnalysis._Model.ModelName + "ResponseDto.cs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string text = $"using {CompanyName}.Util.Applications.Dtos;\r\nusing System.Collections.Generic\r\n" + (string.IsNullOrWhiteSpace(ModelAnalysis._Model.UsingEnum) ? "" : ModelAnalysis._Model.UsingEnum) + ";\r\nnamespace " + ModelAnalysis._Model.Namespace + ".Dtos\r\n~\r\n    # <summary>\r\n    # " + ModelAnalysis._Model.ModelDesc + "返回Dto\r\n    # <!summary>\r\n    public class " + ModelAnalysis._Model.ModelName + "ResponseDto : ResponseBase\r\n    ~\r\n";
            IEnumerable<ModelField> enumerable = ModelAnalysis._Model.ModelField.Where((ModelField x) => x.IsResponse);
            foreach (ModelField item in enumerable)
            {
                text = text + "\r\n        # <summary>\r\n        # " + item.AttrDesc + "\r\n        # <!summary>\r\n";
                text = text + "        public " + item.AttrDataType + " " + item.AttrName + " { get; set; }\r\n";
            }
            text += "\r\n    }\r\n}";
            FileStream fileStream = new FileStream(path2, FileMode.Create);
            byte[] bytes = Encoding.UTF8.GetBytes(text.Replace("#", "///").Replace("!", "/").Replace("^", "\"")
                .Replace("~", "{")
                .Replace("`", "}"));
            fileStream.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        public void CodeingBaseIService()
        {
            string filePath = ModelAnalysis._Model.FilePath;
            string path = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\AppServices\\" + ModelAnalysis._Model.ModelName);
            string path2 = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\AppServices\\" + ModelAnalysis._Model.ModelName + "\\I" + ModelAnalysis._Model.ModelName + "Service.cs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string text = "using System;\r\nusing " + ModelAnalysis._Model.Namespace + $".Dtos;\r\nusing {CompanyName}.Util.Applications;\r\nusing {CompanyName}.Util.Domains.Repositories;\r\nnamespace " + ModelAnalysis._Model.Namespace + ".AppServices\r\n~\r\n    # <summary>\r\n    # " + ModelAnalysis._Model.ModelDesc + "接口\r\n    # <!summary>\r\n    public interface I" + ModelAnalysis._Model.ModelName + "Service : IService\r\n    ~\r\n    `\r\n`";
            FileStream fileStream = new FileStream(path2, FileMode.Create);
            byte[] bytes = Encoding.Default.GetBytes(text.Replace("#", "///").Replace("!", "/").Replace("^", "\"")
                .Replace("~", "{")
                .Replace("`", "}"));
            fileStream.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// service层代码
        /// </summary>
        public void CodeingBaseService()
        {
            string filePath = ModelAnalysis._Model.FilePath;
            string path = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\AppServices\\" + ModelAnalysis._Model.ModelName);
            string path2 = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\AppServices\\" + ModelAnalysis._Model.ModelName + "\\" + ModelAnalysis._Model.ModelName + "Service.cs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string text = "using System;\r\nusing " + ModelAnalysis._Model.Namespace + $".Dtos;\r\nusing {CompanyName}.Util.Maps;\r\nnamespace " + ModelAnalysis._Model.Namespace + ".AppServices\r\n~\r\n    # <summary>\r\n    # " + ModelAnalysis._Model.ModelDesc + "服务\r\n    # <!summary>\r\n    public class " + ModelAnalysis._Model.ModelName + "Service : I" + ModelAnalysis._Model.ModelName + "Service\r\n    ~\r\n    `\r\n`";
            FileStream fileStream = new FileStream(path2, FileMode.Create);
            byte[] bytes = Encoding.UTF8.GetBytes(text.Replace("#", "///").Replace("!", "/").Replace("^", "\"")
                .Replace("~", "{")
                .Replace("`", "}"));
            fileStream.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// service接口层代码
        /// </summary>
        public void CodeingIService()
        {
            string filePath = ModelAnalysis._Model.FilePath;
            string path = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\AppServices\\" + ModelAnalysis._Model.ModelName);
            string path2 = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\AppServices\\" + ModelAnalysis._Model.ModelName + "\\I" + ModelAnalysis._Model.ModelName + "Service.cs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string text = $"using {CompanyName}.Applications;\r\nusing " + ModelAnalysis._Model.Namespace + ".Dtos;\r\nnamespace " + ModelAnalysis._Model.Namespace + ".AppServices\r\n~\r\n    # <summary>\r\n    # " + ModelAnalysis._Model.ModelDesc + "接口\r\n    # <!summary>\r\n    public interface I" + ModelAnalysis._Model.ModelName + "Service : ICrudService<" + ModelAnalysis._Model.ModelName + "ResponseDto, " + ModelAnalysis._Model.ModelName + "CreateDto, " + ModelAnalysis._Model.ModelName + "UpdateDto, " + ModelAnalysis._Model.ModelName + "QueryDto>\r\n    ~\r\n    `\r\n`";
            FileStream fileStream = new FileStream(path2, FileMode.Create);
            byte[] bytes = Encoding.UTF8.GetBytes(text.Replace("#", "///").Replace("!", "/").Replace("^", "\"")
                .Replace("~", "{")
                .Replace("`", "}"));
            fileStream.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// service代码生成
        /// </summary>
        public void CodeingService()
        {
            string filePath = ModelAnalysis._Model.FilePath;
            string path = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\AppServices\\" + ModelAnalysis._Model.ModelName);
            string path2 = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\AppServices\\" + ModelAnalysis._Model.ModelName + "\\" + ModelAnalysis._Model.ModelName + "Service.cs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string text = $"using {CompanyName}.Applications;\r\nusing System;\r\nusing {CompanyName}.Util.Datas.UnitOfWorks;\r\nusing {CompanyName}.Util.Domains.Repositories;\r\nusing " + ModelAnalysis._Model.Namespace + ".Dtos;\r\nnamespace " + ModelAnalysis._Model.Namespace + ".AppServices\r\n~\r\n    # <summary>\r\n    # " + ModelAnalysis._Model.ModelDesc + "服务\r\n    # <!summary>\r\n    public class " + ModelAnalysis._Model.ModelName + "Service : CrudServiceBase<Domains.Entities." + ModelAnalysis._Model.ModelName + "," + ModelAnalysis._Model.ModelName + "ResponseDto, " + ModelAnalysis._Model.ModelName + "CreateDto, " + ModelAnalysis._Model.ModelName + "UpdateDto, " + ModelAnalysis._Model.ModelName + "QueryDto" + ((ModelAnalysis._Model.BaseEntities.IndexOf("Long") > -1) ? ",long" : "Guid") + ">, I" + ModelAnalysis._Model.ModelName + "Service\r\n    ~\r\n        public " + ModelAnalysis._Model.ModelName + "Service(IUnitOfWork unitOfWork, IRepository" + ((ModelAnalysis._Model.BaseEntities.IndexOf("Long") > -1) ? "Long" : "") + "<Domains.Entities." + ModelAnalysis._Model.ModelName + "> repository) : base(unitOfWork, repository)\r\n        ~\r\n        `\r\n    `\r\n`";
            FileStream fileStream = new FileStream(path2, FileMode.Create);
            byte[] bytes = Encoding.UTF8.GetBytes(text.Replace("#", "///").Replace("!", "/").Replace("^", "\"")
                .Replace("~", "{")
                .Replace("`", "}"));
            fileStream.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// 
        /// </summary>
        public void CodeingBaseController()
        {
            string filePath = ModelAnalysis._Model.FilePath;
            string path = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\Controllers");
            string path2 = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\Controllers\\" + ModelAnalysis._Model.ModelName + "Controller.cs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string text = $"using {CompanyName}.Webs.Controllers;\r\nusing " + ModelAnalysis._Model.Namespace + ".Dtos;\r\nusing " + ModelAnalysis._Model.Namespace + ".AppServices\r\nusing Microsoft.AspNetCore.Mvc;\r\nnamespace " + ModelAnalysis._Model.Namespace + ".AppServices\r\n~\r\n    # <summary>\r\n    # " + ModelAnalysis._Model.ModelDesc + "管理\r\n    # <!summary>\r\n    public class " + ModelAnalysis._Model.ModelName + "Controller : WebApiControllerBase\r\n    ~\r\n        private readonly I" + ModelAnalysis._Model.ModelName + "Service _service;\r\n        public " + ModelAnalysis._Model.ModelName + "Controller(I" + ModelAnalysis._Model.ModelName + "Service service)\r\n        ~\r\n            _service = service;\r\n        `\r\n    `\r\n`";
            FileStream fileStream = new FileStream(path2, FileMode.Create);
            byte[] bytes = Encoding.UTF8.GetBytes(text.Replace("#", "///").Replace("!", "/").Replace("^", "\"")
                .Replace("~", "{")
                .Replace("`", "}"));
            fileStream.Write(bytes, 0, bytes.Length);
        }

        /// <summary>
        /// Controller 代码生成
        /// </summary>
        public void CodeingController()
        {
            string filePath = ModelAnalysis._Model.FilePath;
            string path = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\Controllers");
            string path2 = filePath.Substring(0, filePath.LastIndexOf("\\")).Replace("\\Domains\\Entities", "\\Controllers\\" + ModelAnalysis._Model.ModelName + "Controller.cs");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            string text = $"using {CompanyName}.Webs.Controllers;\r\nusing " + ModelAnalysis._Model.Namespace + ".Dtos;\r\nusing " + ModelAnalysis._Model.Namespace + $".AppServices;\r\nusing Microsoft.AspNetCore.Mvc;\r\nusing {CompanyName}.Applications;\r\nnamespace " + ModelAnalysis._Model.Namespace + ".AppServices\r\n~\r\n    # <summary>\r\n    # " + ModelAnalysis._Model.ModelDesc + "管理\r\n    # <!summary>\r\n    public class " + ModelAnalysis._Model.ModelName + "Controller : CrudControllerBase<" + ModelAnalysis._Model.ModelName + "ResponseDto, " + ModelAnalysis._Model.ModelName + "CreateDto, " + ModelAnalysis._Model.ModelName + "UpdateDto, " + ModelAnalysis._Model.ModelName + "QueryDto>\r\n    ~\r\n        public " + ModelAnalysis._Model.ModelName + "Controller(ICrudService<" + ModelAnalysis._Model.ModelName + "ResponseDto, " + ModelAnalysis._Model.ModelName + "CreateDto, " + ModelAnalysis._Model.ModelName + "UpdateDto, " + ModelAnalysis._Model.ModelName + "QueryDto> service) : base(service)\r\n        ~\r\n        `\r\n    `\r\n`";
            FileStream fileStream = new FileStream(path2, FileMode.Create);
            byte[] bytes = Encoding.UTF8.GetBytes(text.Replace("#", "///").Replace("!", "/").Replace("^", "\"")
                .Replace("~", "{")
                .Replace("`", "}"));
            fileStream.Write(bytes, 0, bytes.Length);
        }
    }



    public class ModelAnalysis
    {
        public List<string> Context = new List<string>();

        public static Model _Model { get; set; }

        public bool ReadFile(string path, ref string errorMessage)
        {
            if (!File.Exists(path))
            {
                errorMessage = "访问的文件不存在!";
                return false;
            }
            Init();
            ReadFile(path);
            _Model.FilePath = path;
            for (int i = 0; i < Context.Count; i++)
            {
                string text = Context[i].Replace(" ", "");
                if (string.IsNullOrWhiteSpace(_Model.UsingEnum) && text.IndexOf("using") > -1 && text.IndexOf("Domains.Enums") > -1)
                {
                    _Model.UsingEnum = Context[i];
                }
                else if (string.IsNullOrWhiteSpace(_Model.Namespace) && text.IndexOf("namespace") > -1)
                {
                    _Model.Namespace = text.Replace("namespace", "").Replace(".Domains.Entities", "").Trim();
                    _Model.BaseNamespace = text.Replace("namespace", "").Trim();
                    if (_Model.BaseNamespace.IndexOf("Domains.Entities") == -1)
                    {
                        errorMessage = "该文件命名空间不是实体类的命名空间!";
                        return false;
                    }
                }
                else if (string.IsNullOrWhiteSpace(_Model.ModelName) && text.IndexOf("publicclass") > -1)
                {
                    string[] array = text.Split(':');
                    _Model.ModelName = array[0].Replace(" ", "").Replace("publicclass", "");
                    _Model.BaseEntities = ((array.Length == 2) ? array[1] : "");
                    int num = 0;
                    string text2 = "";
                    while (true)
                    {
                        num++;
                        if (i - num < 0)
                        {
                            break;
                        }
                        string text3 = Context[i - num].Replace(" ", "");
                        if (text3.IndexOf("//") > -1)
                        {
                            text2 = text2 + "," + text3;
                        }
                        else if (text3.IndexOf("{") > -1)
                        {
                            break;
                        }
                    }
                    _Model.ModelDesc = text2.Replace("<summary>", "").Replace("</summary>", "").Replace("/", "")
                        .Trim(',')
                        .Trim();
                }
                else
                {
                    if (text.IndexOf("{get;set;}") <= -1)
                    {
                        continue;
                    }
                    ModelField modelField = new ModelField
                    {
                        IsCreateRequest = true,
                        IsRequired = false,
                        IsResponse = true,
                        IsUpdateRequest = true
                    };
                    string text4 = Context[i].Replace(" public ", "").Replace("{", "").Replace("}", "")
                        .Replace("get;", "")
                        .Replace("virtual", "")
                        .Replace("set;", "")
                        .Trim();
                    string[] array2 = text4.Split(' ');
                    if (array2.Length != 2)
                    {
                        errorMessage = "读取属性“" + text4 + "”失败，请格式化代码";
                        return false;
                    }
                    modelField.AttrDataType = array2[0].Trim();
                    modelField.AttrName = array2[1].Trim();
                    int num2 = 0;
                    string text5 = "";
                    while (true)
                    {
                        num2++;
                        if (i - num2 < 0)
                        {
                            break;
                        }
                        string text6 = Context[i - num2].Replace(" ", "");
                        if (text6.IndexOf("//") > -1)
                        {
                            text5 = text5 + "," + text6;
                        }
                        else if (text6.IndexOf("}") > -1)
                        {
                            break;
                        }
                    }
                    modelField.AttrDesc = text5.Replace("<summary>", "").Replace("</summary>", "").Replace("/", "")
                        .Trim(',')
                        .Trim();
                    _Model.ModelField.Add(modelField);
                }
            }
            if (string.IsNullOrWhiteSpace(_Model.ModelName))
            {
                errorMessage = "读取实体名称失败";
                return false;
            }
            if (string.IsNullOrWhiteSpace(_Model.Namespace))
            {
                errorMessage = "读取命名空间失败";
                return false;
            }
            if (string.IsNullOrWhiteSpace(_Model.BaseEntities))
            {
                errorMessage = "读取基类失败";
                return false;
            }
            SetTableName();
            return true;
        }

        private void ReadFile(string path)
        {
            StreamReader streamReader = File.OpenText(path);
            while (!streamReader.EndOfStream)
            {
                Context.Add(streamReader.ReadLine());
            }
        }

        public void Init()
        {
            _Model = new Model
            {
                IsController = true,
                IsCreateDto = true,
                IsMap = true,
                IsQueryDto = true,
                IsResponseDto = true,
                IsService = true,
                IsUpdateDto = true,
                ModelField = new List<ModelField>()
            };
        }

        public void SetTableName()
        {
            string text = _Model.ModelName.Substring(_Model.ModelName.Length - 1, 1);
            string text2 = _Model.ModelName.Substring(_Model.ModelName.Length - 2, 2);
            if (text2 == "sh" || text2 == "ch" || text == "s" || text == "x")
            {
                _Model.TableName = _Model.ModelName + "es";
            }
            else if (text == "y")
            {
                _Model.TableName = _Model.ModelName.Substring(0, _Model.ModelName.Length - 1) + "ies";
            }
            else
            {
                _Model.TableName = _Model.ModelName + "s";
            }
        }
    }


    /// <summary>
    /// 实体
    /// </summary>
    public class Model
    {
        public string Namespace { get; set; }

        public string BaseNamespace { get; set; }

        public string ModelName { get; set; }

        public string ModelDesc { get; set; }

        public string Prefix { get; set; }

        public string TableName { get; set; }

        public bool IsCreateDto { get; set; }

        public bool IsUpdateDto { get; set; }

        public bool IsQueryDto { get; set; }

        public bool IsResponseDto { get; set; }

        public bool IsMap { get; set; }

        public bool IsService { get; set; }

        public bool IsController { get; set; }

        public List<ModelField> ModelField { get; set; }

        public string UsingEnum { get; set; }

        public string Generic { get; set; }

        public string BaseEntities { get; set; }

        public string FilePath { get; set; }
    }


    /// <summary>
    /// 字段属性
    /// </summary>
    public class ModelField
    {
        public string AttrDesc { get; set; }

        public string AttrName { get; set; }

        public string AttrDataType { get; set; }

        public double? MaximumValue { get; set; }

        public double? MinimumValue { get; set; }

        public bool IsCreateRequest { get; set; }

        public bool IsUpdateRequest { get; set; }

        public bool IsResponse { get; set; }

        public bool IsRequired { get; set; }
    }

}

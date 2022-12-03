using ExcelDataReader;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebAdminTemplate.Data;
using WebAdminTemplate.Model;

namespace WebAdminTemplate.Areas.Admin.Controllers
{
    [Area("admin")]
    [Route("admin/GPF")]
    public class GPFController : Controller
    {
        private readonly DataContext _context;
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _hostEnvironment;
        IExcelDataReader reader;

        public GPFController(DataContext context, IConfiguration configuration, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _configuration = configuration;
            _hostEnvironment = hostEnvironment;
        }
        [Route("")]
        [Route("index")]
        public IActionResult Index()
        {
            IEnumerable<GPFData> list = _context.GPFData.ToList();

            return View(list);
        }
        [Route("create")]
        public IActionResult Create()
        {
            return View();
        }
        [Route("create")]
        [HttpPost]
        public IActionResult Create(GPFModel model)
        {
            GPFData data = new GPFData();
            data.EmployeeId = model.EmployeeId;
            data.ProcessingDate = model.ProcessingDate;
            data.FilePath = model.FilePath;
            data.IntrestRate = model.IntrestRate;
            var employee = _context.Employee.Where(x => x.Id == model.EmployeeId).FirstOrDefault();
            data.MemberContribution = Convert.ToDecimal((employee.Salary * model.IntrestRate) / 100);
            var gpfData = _context.GPFData.Where(x => x.EmployeeId == model.EmployeeId && x.ProcessingDate < model.ProcessingDate).ToList();
            if (gpfData.Count() > 0)
            {
                data.GPFAmount = gpfData.Sum(x => x.MemberContribution) + data.MemberContribution;
            }
            else
            {
                data.GPFAmount = data.MemberContribution;
            }
            data.EmployeeType = model.EmployeeType;
            _context.GPFData.Add(data);
            _context.SaveChanges();
            return View(model);
        }
        [Route("onFileUpload")]
        [HttpPost]
        [IgnoreAntiforgeryToken]
        public IActionResult OnFileUpload([FromForm] IFormFile uploadedFile)
        {
            if (uploadedFile == null)
            {
                return Json(new { data = "Files missing" });
            }

            if (Request.Form.Files.Count > 0)
            {
                string path = Path.Combine(_hostEnvironment.WebRootPath, "Uploads");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                string fileName = Path.GetFileName(uploadedFile.FileName);
                string filePath = Path.Combine(path, fileName);

                using (FileStream stream = new FileStream(filePath, FileMode.Create))
                {
                    uploadedFile.CopyTo(stream);
                }

                //string csvData = System.IO.File.ReadAllText(filePath);

                //DataTable dt = new DataTable();
                //bool firstRow = true;

                //try
                //{
                //    foreach (string row in csvData.Split('\n'))
                //    {
                //        if (!string.IsNullOrEmpty(row))
                //        {
                //            if (!string.IsNullOrEmpty(row))
                //            {
                //                if (firstRow)
                //                {
                //                    foreach (string cell in row.Split(','))
                //                    {
                //                        dt.Columns.Add(cell.Trim());
                //                    }

                //                    firstRow = false;
                //                }
                //                else
                //                {
                //                    dt.Rows.Add();
                //                    int i = 0;

                //                    foreach (string cell in row.Split(','))
                //                    {
                //                        dt.Rows[dt.Rows.Count - 1][i] = cell.Trim();
                //                        i++;
                //                    }
                //                }
                //            }
                //        }
                //    }

                //}
                //catch (Exception ex)
                //{ }

                return Json(filePath);
            }
            else
            {
                return Json(new { data = "File Missing" });
            }
        }


        [HttpPost]
        public async Task<IActionResult> UploadExel(IFormFile file)
        {
            try
            {
                // Check the File is received

                if (file == null)
                    throw new Exception("File is Not Received...");


                // Create the Directory if it is not exist
                string dirPath = Path.Combine(_hostEnvironment.WebRootPath, "ReceivedReports");
                if (!Directory.Exists(dirPath))
                {
                    Directory.CreateDirectory(dirPath);
                }

                // MAke sure that only Excel file is used 
                string dataFileName = Path.GetFileName(file.FileName);

                string extension = Path.GetExtension(dataFileName);

                string[] allowedExtsnions = new string[] { ".xls", ".xlsx" };

                if (!allowedExtsnions.Contains(extension))
                    throw new Exception("Sorry! This file is not allowed, make sure that file having extension as either.xls or.xlsx is uploaded.");

                // Make a Copy of the Posted File from the Received HTTP Request
                string saveToPath = Path.Combine(dirPath, dataFileName);

                using (FileStream stream = new FileStream(saveToPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }

                // USe this to handle Encodeing differences in .NET Core
                System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                // read the excel file
                using (var stream = new FileStream(saveToPath, FileMode.Open))
                {
                    if (extension == ".xls")
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);
                    else
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);

                    DataSet ds = new DataSet();
                    ds = reader.AsDataSet();
                    reader.Close();

                    if (ds != null && ds.Tables.Count > 0)
                    {
                        // Read the the Table
                        DataTable serviceDetails = ds.Tables[0];
                        for (int i = 1; i < serviceDetails.Rows.Count; i++)
                        {
                            //CustomerResponseDetail details = new CustomerResponseDetail();
                            //details.ServiceEngineerName = serviceDetails.Rows[i][0].ToString();
                            //details.CustomerName = serviceDetails.Rows[i][1].ToString();
                            //details.Address = serviceDetails.Rows[i][2].ToString();
                            //details.City = serviceDetails.Rows[i][3].ToString();
                            //details.ComplaintType = serviceDetails.Rows[i][4].ToString();
                            //details.DeviceName = serviceDetails.Rows[i][5].ToString();
                            //details.ComplaintDate = Convert.ToDateTime(serviceDetails.Rows[i][6].ToString());
                            //details.VisitDate = Convert.ToDateTime(serviceDetails.Rows[i][7].ToString());
                            //details.ComplaintDetails = serviceDetails.Rows[i][8].ToString();
                            //details.RepairDetails = serviceDetails.Rows[i][9].ToString();
                            //details.ResolveDate = Convert.ToDateTime(serviceDetails.Rows[i][10].ToString());

                            //details.Fees = Convert.ToDecimal(serviceDetails.Rows[i][11].ToString());
                            //details.UploadDate = DateTime.Now;


                            //// Add the record in Database
                            //await context.CustomerResponseDetails.AddAsync(details);
                            //await context.SaveChangesAsync();
                        }
                    }
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View();
                //return View("Error", new ErrorViewModel()
                //{
                //    ControllerName = this.RouteData.Values["controller"].ToString(),
                //    ActionName = this.RouteData.Values["action"].ToString(),
                //    ErrorMessage = ex.Message
                //});
            }
        }

    }
}

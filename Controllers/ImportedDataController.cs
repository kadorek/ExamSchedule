using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ExamSchedule.Models;
using System.IO;
using NPOI.XSSF.UserModel;
using NPOI.SS.UserModel;
using System.Text;
using NPOI.HSSF.UserModel;
using ExamSchedule.Models.ViewModels;
using ExamSchedule.Extensions;
using Newtonsoft.Json;
using System.Net;

namespace ExamSchedule.Controllers
{
	public class ImportedDataController : Controller
	{
		private readonly examdataContext _context;

		public ImportedDataController(examdataContext context)
		{
			_context = context;
		}

		// GET: ImportedData
		public async Task<IActionResult> Index()
		{
			return View(await _context.ImportedDatas.ToListAsync());
		}

		// GET: ImportedData/Details/5
		public async Task<IActionResult> Details(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var importedData = await _context.ImportedDatas
				.FirstOrDefaultAsync(m => m.Id == id);
			if (importedData == null)
			{
				return NotFound();
			}

			return View(importedData);
		}

		// GET: ImportedData/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: ImportedData/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(Models.ViewModels.ImportDataViewModel idvm)
		{

			if (ModelState.IsValid)
			{
				ImportedData _imported = new ImportedData();
				_imported.Date = DateTime.Now.ToString();
				_imported.MIMEType = idvm.DataFile.ContentType;
				MemoryStream ms = new MemoryStream();
				await idvm.DataFile.CopyToAsync(ms);
				_imported.Data = ms.ToArray();
				_context.Add(_imported);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(idvm);
		}

		// GET: ImportedData/Edit/5
		public async Task<IActionResult> Edit(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var importedData = await _context.ImportedDatas.FindAsync(id);
			if (importedData == null)
			{
				return NotFound();
			}
			return View(importedData);
		}

		// POST: ImportedData/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(long id, [Bind("Id,Date,MIMEType,Data")] ImportedData importedData)
		{
			if (id != importedData.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(importedData);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ImportedDataExists(importedData.Id))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(importedData);
		}

		// GET: ImportedData/Delete/5
		public async Task<IActionResult> Delete(long? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var importedData = await _context.ImportedDatas
				.FirstOrDefaultAsync(m => m.Id == id);
			if (importedData == null)
			{
				return NotFound();
			}

			return View(importedData);
		}

		// POST: ImportedData/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(long id)
		{
			var importedData = await _context.ImportedDatas.FindAsync(id);
			_context.ImportedDatas.Remove(importedData);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool ImportedDataExists(long id)
		{
			return _context.ImportedDatas.Any(e => e.Id == id);
		}

		public async Task<IActionResult> Use(long? id)
		{

			if (id == null)
			{
				return NotFound();
			}

			var impD = await _context.ImportedDatas.FindAsync(id);

			if (impD == null)
			{
				return NotFound();
			}

			Models.ViewModels.ImportedDataUseViewModel useModel = new Models.ViewModels.ImportedDataUseViewModel();
			useModel.DataString = "";// ReadExcelData(impD.Data);
			useModel.ImportedDataId = impD.Id;

			return View(useModel);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Use(string ImportedDataId, string Selection)
		{


			//var ImportedDataId = Request;
			long id = Convert.ToInt64(ImportedDataId);
			//if (ModelState.IsValid)
			//{
			//	RedirectToAction("ImportDataCheck",idcm.Selection);
			//}

			return View("Use", ImportedDataId);

		}



		public string ReadExcelData(byte[] _data)
		{

			StringBuilder sb = new StringBuilder();
			MemoryStream ms = new MemoryStream(_data);
			ms.Position = 0;

			HSSFWorkbook wbook = new HSSFWorkbook(ms);
			ISheet sheet = wbook.GetSheetAt(0);

			IRow headerRow = sheet.GetRow(0);
			int cellCount = headerRow.LastCellNum;
			sb.Append("<table class=\"table\" id=\"tableData\">");
			sb.Append("<tr>");
			for (int i = 0; i < cellCount; i++)
			{
				sb.Append("<th></th>");
			}
			sb.Append("</tr>");
			sb.Append("<tr>");
			for (int i = 0; i < cellCount; i++)
			{
				ICell _cell = headerRow.GetCell(i);
				if (_cell == null || string.IsNullOrWhiteSpace(_cell.ToString())) continue;
				sb.AppendFormat("<th>{0}</th>", _cell.ToString());
			}
			sb.Append("</tr>");
			sb.Append("<tbody class=\"data-body\">");
			for (int j = sheet.FirstRowNum + 1; j <= sheet.LastRowNum; j++)
			{
				IRow _r = sheet.GetRow(j);
				if (_r == null) continue;
				if (_r.Cells.All(x => x.CellType == CellType.Blank)) continue;
				sb.Append("<tr>");
				for (int k = _r.FirstCellNum; k < cellCount; k++)
				{
					if (_r.GetCell(k) != null)
					{
						sb.AppendFormat("<td>{0}</td>", _r.GetCell(k).ToString());
					}
				}
				sb.Append("</tr>");
			}
			sb.Append("</tbody>");
			sb.Append("</table>");

			return sb.ToString();
		}


		public IActionResult PropertySelect(byte type, int index)
		{

			Type _t = null;

			PropertySelectModel psm = new PropertySelectModel();
			psm.CellIndex = index;
			switch (type)
			{
				case 1:
					_t = typeof(Teacher);
					break;
				case 2:
					_t = typeof(Course);
					break;
				case 3:
					_t = typeof(Programme);
					break;
				case 4:
					_t = typeof(Student);
					break;
				case 5:
					_t = typeof(Room);
					break;
				case 6:
					_t = typeof(TeacherCourse);
					break;
				case 7:
					_t = typeof(ProgramCourse);
					break;
				case 8:
					_t = typeof(CourseStudent);
					break;
				default:
					break;
			}

			var allPublicProps = _t.GetProperties();
			var restrictedPropNames = _t.GetAttributeValue((HideFromSelect _h) => _h.ProppertyNames);

			foreach (var item in allPublicProps)
			{
				if (!restrictedPropNames.Contains(item.Name))
				{
					psm.PropertyNames.Add(item.Name);
				}
			}

			//var allPublicProps = typeof(Programme).GetProperties(System.Reflection.BindingFlags.Public);
			//var restrictedPropNames=typeof(Programme).GetAttributeValue((HideFromSelect _h)=>_h.ProppertyNames);


			return PartialView("_PropertySelect", psm);

		}

		public async Task<IActionResult> GetDataTable(long importedDataId)
		{

			var impD = await _context.ImportedDatas.FindAsync(importedDataId);
			DataTableModel dtm = new DataTableModel();
			dtm.ImportedDataId = impD.Id;

			MemoryStream ms = new MemoryStream(impD.Data);
			ms.Position = 0;

			HSSFWorkbook wbook = new HSSFWorkbook(ms);
			ISheet sheet = wbook.GetSheetAt(0);

			IRow headerRow = sheet.GetRow(0);
			int cellCount = headerRow.LastCellNum;

			for (int i = 0; i < cellCount; i++)
			{
				ICell _cell = headerRow.GetCell(i);
				if (_cell == null || string.IsNullOrWhiteSpace(_cell.ToString())) continue;
				dtm.TableHeaders.Add(_cell.ToString());
			}

			for (int i = 0; i < cellCount; i++)
			{
				IRow _r = sheet.GetRow(i);
				if (_r == null) continue;
				if (_r.Cells.All(x => x.CellType == CellType.Blank)) continue;
				for (int k = _r.FirstCellNum; k < cellCount; k++)
				{
					List<string> values = new List<string>();
					if (_r.GetCell(k) != null)
					{
						values.Add(_r.GetCell(k).ToString());
					}
					dtm.ValuesAsRows.Add(values);
				}

			}

			return PartialView("_GetDataTable", dtm);
		}

		public IActionResult ImportDataCheck(ImportDataCheckModel idcm)
		{

			return View("ImportCheck", idcm);
		}

		//public IActionResult Sample() {

		//	return View("Sample");
		//}
		[HttpPost]
		//[JsonFilter(JsonDataType = typeof(WillImportModel), Param = "wim")]
		public async Task<JsonResult> InsertImportedData([FromBody] WillImportModel wim)
		{

			if (wim == null)
			{
				return Json(new
				{
					Type = "Error",
					Message = "Eklenen öğe listesi boş.",
					StatusCode = HttpStatusCode.BadRequest,
					InnerMessage = ""
				});
			}
			else
			{
				foreach (var item in wim.List)
				{
					try
					{
						switch (Convert.ToByte(wim.ModelName))
						{
							case 1:
								//_t = typeof(Teacher);
								var _tx = JsonConvert.DeserializeObject<Teacher>(item.ToString());
								if (_tx.LastName == null)
								{
									var _s = "";
									var slices = _tx.Name.Split(" ");
									for (int i = 0; i < slices.Length; i++)
									{
										if (slices[i].All(x => char.IsUpper(x)))
										{
											_s += slices[i] + " ";
										}
									}
									_s = _s.Trim();
									_tx.Name = _tx.Name.Replace(_s, "");
									_tx.LastName = _s;
									_tx.Name = _tx.Name.Trim();
								}

								if (_tx.Name == null)
								{
									var _s = "";
									var slices = _tx.LastName.Split(" ");
									for (int i = 0; i < slices.Length; i++)
									{
										if (slices[i].All(x => char.IsUpper(x)))
										{
											continue;
										}
										else
										{
											_s += slices[i] + " ";
										}
									}
									_s = _s.Trim();
									_tx.LastName = _tx.LastName.Replace(_s, "");
									_tx.Name = _s;
									_tx.LastName = _tx.LastName.Trim();

								}
								await _context.AddAsync(_tx);
								break;
							case 2:
								//_t = typeof(Course);
								await _context.AddAsync(JsonConvert.DeserializeObject<Course>(item.ToString()));
								break;
							case 3:
								//_t = typeof(Programme);
								await _context.AddAsync(JsonConvert.DeserializeObject<Programme>(item.ToString()));
								break;
							case 4:
								await _context.AddAsync(JsonConvert.DeserializeObject<Student>(item.ToString()));
								break;
							case 5:
								await _context.AddAsync(JsonConvert.DeserializeObject<Room>(item.ToString()));
								break;
							case 6:
								var _tcx = JsonConvert.DeserializeObject<TeacherCourse>(item.ToString());

								if (_tcx.ProgrammeName != null)
								{
									var _p = await _context.Programmes.FirstOrDefaultAsync(x => x.Name == _tcx.ProgrammeName);
									if (_p == null)
									{
										throw new Exception("Program bilgisi bulunamadı.");
									}
									_tcx.ProgrammeId = _p.Id;
								}
								else
								{
									throw new Exception("Program bilgisi bulunamadı.");
								}


								if (_tcx.TeacherName == null && _tcx.TeacherFullName == null)
								{
									throw new Exception("Hoca bilgisi bulunamadı.");
								}
								else
								{
									var _tt = await _context
										.Teachers
										.FirstOrDefaultAsync(x => _tcx.TeacherFullName.Contains(x.Name) && _tcx.TeacherFullName.Contains(x.LastName));

									if (_tt == null)
									{
										_tt = await _context
										.Teachers
										.FirstOrDefaultAsync(x => x.Name == _tcx.TeacherName);
									}

									if (_tt == null)
									{
										throw new Exception("Hoca bilgisi bulunamadı.");
									}
									else
									{
										_tcx.TeacherId = _tt.Id;
									}

								}

								if (_tcx.AllIsNull("CourseUKey", "CourseName"))
								{
									throw new Exception("Ders bilgisi bulunamadı.");
								}

								var _c = new Course();
								if (_tcx.CourseUKey != null)
								{
									_c = await _context.Courses.FirstOrDefaultAsync(x => x.Ukey == _tcx.CourseUKey);
								}
								else if (_tcx.CourseName != null)
								{
									_c = await _context.Courses.FirstOrDefaultAsync(x => x.Name == _tcx.CourseName);
								}

								if (_c == null)
								{
									throw new Exception("Ders bilgisi bulunamadı.");
								}
								else
								{
									_tcx.CourseId = _c.Id;
								}

								await _context.AddAsync(_tcx);
								break;
							case 7:
								var _pcx = JsonConvert.DeserializeObject<ProgramCourse>(item.ToString());

								if (_pcx.AllIsNull("ProgrammeName"))
								{
									throw new Exception("Program bilgisi bulunamadı.");
								}
								else
								{
									var _p = await _context.Programmes.FirstOrDefaultAsync(x => x.Name == _pcx.ProgrammeName);
									if (_p == null)
									{
										throw new Exception("Program adı bulunamadı.");
									}

									_pcx.ProgramId = _p.Id;

								}

								if (_pcx.AllIsNull("CourseName", "CourseUKey"))
								{
									throw new Exception("Ders bilgisi bulunamadı");
								}
								else
								{
									var _cx = new Course();
									if (_pcx.CourseUKey != null)
									{
										_cx = await _context.Courses.FirstOrDefaultAsync(x => x.Ukey == _pcx.CourseUKey);
									}
									else if (_pcx.CourseName != null)
									{
										_cx = await _context.Courses.FirstOrDefaultAsync(x => x.Name == _pcx.CourseName);
									}

									if (_cx == null)
									{
										throw new Exception("Ders kısa adı bilgisi ile ders bulunamadı.");
									}
									_pcx.CourseId = _cx.Id;
								}

								await _context.AddAsync(_pcx);

								break;

							case 8:

								var _csx = JsonConvert.DeserializeObject<CourseStudent>(item.ToString());

								if (_csx.StudentNo == null)
								{
									throw new Exception("Gönderilen öğrenci bilgisi hatalı");
								}
								else
								{
									var std = await _context.Students.FirstOrDefaultAsync(x => x.StudentNo == _csx.StudentNo);
									if (std == null)
									{
										throw new Exception("Aranan öğrenci bulunamadı. Öğrenci no : " + _csx.StudentNo);
									}
									_csx.StudentId = std.Id;
								}

								if (_csx.AllIsNull("CourseName", "CourseUKey"))
								{
									throw new Exception("Gönderilen ders bilgisi hatalı.");
								}
								else
								{
									var _cxx = new Course();
									if (_csx.CourseUKey != null)
									{
										_cxx = await _context.Courses.FirstOrDefaultAsync(x => x.Ukey == _csx.CourseUKey);
									}
									else if (_csx.CourseName != null)
									{
										_cxx = await _context.Courses.FirstOrDefaultAsync(x => x.Name == _csx.CourseName);
									}
									if (_cxx == null)
									{
										throw new Exception("Gönderilen bilgilerle Ders bulunamadı.");
									}

									_csx.CourseId = _cxx.Id;
								}

								await _context.AddAsync(_csx);

								break;

							default:
								throw new Exception("Gönderilen bilgi içeriğinde tanımlanamayan bilgiler var.");

						}


					}
					catch (Exception ex)
					{
						return Json(new
						{
							Type = "Error",
							Message = "Beklenmeyen hata oluştu.",
							StatusCode = HttpStatusCode.Conflict,
							InnerMessage = ex.Message + "\n" + (ex.InnerException == null ? "" : ex.InnerException.Message)
						});
					}
				}
				try
				{
					await _context.SaveChangesAsync();
				}
				catch (Exception ex)
				{
					return Json(new
					{
						Type = "Error",
						Message = "Beklenmeyen hata oluştu.",
						StatusCode = HttpStatusCode.Conflict,
						InnerMessage = ex.Message + "\n" + ex.InnerException.Message
					});
				}

				return Json(new
				{
					Type = "Success",
					Message = "Öğeler başarılı şekilde eklendi.",
					StatusCode = HttpStatusCode.OK,
					InnerMessage = ""
				});

			}
		}

	}
}

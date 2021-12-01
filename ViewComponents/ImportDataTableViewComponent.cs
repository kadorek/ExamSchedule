using ExamSchedule.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ExamSchedule.ViewComponents
{
	[ViewComponent(Name = "ImportDataTable")]
	public class ImportDataTableViewComponent : ViewComponent
	{
		private readonly ExamSchedule.Models.examdataContext _context;

		public ImportDataTableViewComponent(Models.examdataContext _c)
		{
			this._context = _c;
		}

		public async Task<IViewComponentResult> InvokeAsync(long importedDataId)
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

			for (int i = sheet.FirstRowNum + 1; i < sheet.LastRowNum; i++)
			{
				IRow _r = sheet.GetRow(i);
				if (_r == null) continue;
				if (_r.Cells.All(x => x.CellType == CellType.Blank)) continue;
				List<string> values = new List<string>();
				for (int k = _r.FirstCellNum; k < cellCount; k++)
				{
					if (_r.GetCell(k) != null)
					{
						values.Add(_r.GetCell(k).ToString());
					}
				}

				dtm.ValuesAsRows.Add(values);
			}

			return View(dtm);

		}

	}
}

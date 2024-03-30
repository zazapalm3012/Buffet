using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Buffet.Models;

public partial class Customer
{
	[Key]
	[Required(ErrorMessage = "ต้องระบุรหัส")]
	[Display(Name = "รหัส")]
	public string CusId { get; set; } = null!;

	[Required(ErrorMessage = "ต้องระบุชื่อ")]
	[Display(Name = "ชื่อ นามสกุล")]
	public string CusName { get; set; } = null!;

	[Required(ErrorMessage = "ต้องระบุรหัสผ่าน")]
	[Display(Name = "รหัสผ่าน")]
	public string CusPass { get; set; } = null!;

	[Required(ErrorMessage = "ต้องระบุE-mail")]
	[Display(Name = "E-mail")]
	public string CusEmail { get; set; } = null!;

	[Required(ErrorMessage = "ต้องระบุเบอร์โทร")]
	[Display(Name = "เบอร์โทร")]
	public string CusPhone { get; set; } = null!;

	public string? CusImg { get; set; }
}

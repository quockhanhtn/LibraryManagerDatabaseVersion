namespace LibraryManager.EntityFramework.Model.DataTransferObject
{
	public class MemberDTO : Member
	{
		public string FullName { get { return this.LastName + " " + this.FirstName; } }
		public string Note { get { return (this.Status == true) ? "Hoạt động" : "Bị khóa"; } }
		public MemberDTO() : base() { }

		public MemberDTO(Member memberRaw) : base()
		{
			this.Id = memberRaw.Id;
			this.FirstName = memberRaw.FirstName;
			this.LastName = memberRaw.LastName;
			this.Birthday = memberRaw.Birthday;
			this.Sex = memberRaw.Sex;
			this.SSN = memberRaw.SSN;
			this.Address = memberRaw.Address;
			this.PhoneNumber = memberRaw.PhoneNumber;
			this.Email = memberRaw.Email;

			this.RegisterDate = memberRaw.RegisterDate;
			this.Status = memberRaw.Status;
		}

		public Member GetBaseModel()
		{
			return new Member()
			{
				Id = this.Id,
				FirstName = this.FirstName,
				LastName = this.LastName,
				Birthday = this.Birthday,
				Sex = this.Sex,
				SSN = this.SSN,
				Address = this.Address,
				PhoneNumber = this.PhoneNumber,
				Email = this.Email,

				RegisterDate = this.RegisterDate,
				Status = this.Status,
			};
		}
	}
}

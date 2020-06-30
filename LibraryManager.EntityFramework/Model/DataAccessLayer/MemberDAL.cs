using LibraryManager.EntityFramework.Model.DataTransferObject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManager.EntityFramework.Model.DataAccessLayer
{
    /// <summary>
    /// Class Data Access Layer for Member
    /// </summary>
    public class MemberDAL
    {
        public static MemberDAL Instance { get => (instance == null) ? new MemberDAL() : instance; }
        private MemberDAL() { }

        public ObservableCollection<MemberDTO> GetList(EnumStatus statusFillter = EnumStatus.AllStatus)
        {
            var listRaw = new List<Member>();
            switch (statusFillter)
            {
                case EnumStatus.AllStatus:
                    listRaw = DataProvider.Instance.Database.Members.ToList();
                    break;
                case EnumStatus.Active:
                    listRaw = DataProvider.Instance.Database.Members.Where(x => x.Status == true).ToList();
                    break;
                case EnumStatus.InActive:
                    listRaw = DataProvider.Instance.Database.Members.Where(x => x.Status == false).ToList();
                    break;
            }
            var listMemberDTO = new ObservableCollection<MemberDTO>();

            foreach (var mem in listRaw) { listMemberDTO.Add(new MemberDTO(mem)); }

            return listMemberDTO;
        }

        public void Add(MemberDTO newMember)
        {
            var newMem = newMember.GetBaseModel();
            DataProvider.Instance.SaveEntity(newMem, EntityState.Added);
        }

        public void Update(MemberDTO member)
        {
            var memberUpdate = DataProvider.Instance.Database.Members.Where(x => x.Id == member.Id).SingleOrDefault();
            if (memberUpdate != null)
            {
                memberUpdate.LastName = member.LastName;
                memberUpdate.FirstName = member.FirstName;
                memberUpdate.Sex = member.Sex;
                memberUpdate.Birthday = member.Birthday;
                memberUpdate.SSN = member.SSN;
                memberUpdate.Address = member.Address;
                memberUpdate.Email = member.Email;
                memberUpdate.PhoneNumber = member.PhoneNumber;

                memberUpdate.RegisterDate = member.RegisterDate;
                
                DataProvider.Instance.SaveEntity(memberUpdate, EntityState.Modified);
            }
        }

        public void ChangeStatus(string idMember)
        {
            var memberUpdate = DataProvider.Instance.Database.Members.Where(x => x.Id == idMember).SingleOrDefault();

            if (memberUpdate != null)
            {
                memberUpdate.Status = (memberUpdate.Status == true) ? false : true;
                DataProvider.Instance.SaveEntity(memberUpdate, EntityState.Modified);
            }
        }

        public Member GetMember(string idMember)
        {
            return DataProvider.Instance.Database.Members.Where(x => x.Id == idMember).SingleOrDefault();
        }

        private static MemberDAL instance;
    }
}

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

        public ObservableCollection<MemberDTO> GetList()
        {
            var listRaw = DataProvider.Instance.Database.Members.ToList();
            var listMemberDTO = new ObservableCollection<MemberDTO>();

            foreach (var mem in listRaw) { listMemberDTO.Add(new MemberDTO(mem)); }

            return listMemberDTO;
        }

        public ObservableCollection<MemberDTO> GetList(bool status)
        {
            var listRaw = DataProvider.Instance.Database.Members.Where(x => x.Status == status).ToList();
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

        private static MemberDAL instance;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelBadgers.Data;
using TravelBadgers.Models;

namespace TravelBadgers.Services
{
    public class MemberService
    {
        private readonly Guid _userId;

        public MemberService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateMember(MemberCreate model)
        {

            using (var ctx = new ApplicationDbContext())
            {
                List<Member> members = ctx.Members.ToList();
                foreach(Member memberInLoop in members)
                {
                    if(memberInLoop.OwnerId == _userId)
                    {
                        return false;
                    }
                }
            }
            var entity =
            new Member()
            {
                OwnerId = _userId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                StreetAddress = model.StreetAddress,
                City = model.City,
                State = model.State,
                ZipCode = model.ZipCode,
                PhoneNumber = model.PhoneNumber,
                DateJoined = DateTime.Now
            };

            using (var ctx = new ApplicationDbContext())
            {
                
                ctx.Members.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        }



        public IEnumerable<MemberListItem> GetMemberList()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Members
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                        e=>
                    new MemberListItem
                    {
                        MemberId = e.MemberId,
                        FirstName = e.FirstName,
                        LastName = e.LastName,
                        DateJoined = e.DateJoined
                    });

                return entity.ToArray();
            }
        }

        public MemberDetail GetMember()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Members
                        .Single(e => e.OwnerId == _userId);
                return
                    new MemberDetail
                    {
                        MemberId = entity.MemberId,
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        StreetAddress = entity.StreetAddress,
                        City = entity.City,
                        State = entity.State,
                        ZipCode = entity.ZipCode,
                        PhoneNumber = entity.PhoneNumber,
                        DateJoined = entity.DateJoined
                    };
            }
        }

        public bool UpdateMember(MemberEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Members
                        .Single(e => e.OwnerId == _userId);

                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.StreetAddress = model.StreetAddress;
                entity.City = model.City;
                entity.State = model.State;
                entity.ZipCode = model.ZipCode;
                entity.PhoneNumber = model.PhoneNumber;

                return ctx.SaveChanges() == 1;
            }
        }
    }
}

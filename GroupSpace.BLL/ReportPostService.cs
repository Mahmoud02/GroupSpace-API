using AutoMapper;
using GroupSpace.BLL.Models.Group;
using GroupSpace.BLL.Shared;
using GroupSpace.DAL.Entities;
using GroupSpace.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupSpace.BLL
{
    public interface IReportPostService
    {
        Response Add(ReportPostInsertDto entity);
        ReportPostDto Get(int reportPostId);
        bool CheckIfReportPostExist(int reportPostId);
        Response Delete(int reportPostId);
        IEnumerable<ReportPostDto> GetReportedPosts(int groupId);
    }
    public class ReportPostService : IReportPostService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;

        public ReportPostService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public Response Add(ReportPostInsertDto entity)
        {
            var reportPost = unitOfWork.ReportPostRepository.Find(g => g.GroupId == entity.GroupId && g.PostId == entity.PostId).FirstOrDefault();
            if (reportPost == null)
            {
                entity.NumOfTimes++;
                var report = _mapper.Map<ReportPost>(entity);
                unitOfWork.ReportPostRepository.Add(report);
                var reslut = unitOfWork.SaveChanges();
                var response = _mapper.Map<Response>(reslut);
                return response;
            }
            else {
                reportPost.NumOfTimes++;
                var reslut = unitOfWork.SaveChanges();
                var response = _mapper.Map<Response>(reslut);
                return response;

            }

        }
        public bool CheckIfReportPostExist(int reportPostId)
        {
            return unitOfWork.ReportPostRepository.CheckIfEntityExist(reportPostId);
        }

        public Response Delete(int reportPostId)
        {
            var reportPost = unitOfWork.ReportPostRepository.Get(reportPostId);
            unitOfWork.ReportPostRepository.Delete(reportPost);
            var reslut = unitOfWork.SaveChanges();
            var response = _mapper.Map<Response>(reslut);
            return response;
        }

        public ReportPostDto Get(int reportPostId)
        {
            var reportPost = unitOfWork.ReportPostRepository.Get(reportPostId);
            var reportPostDto = _mapper.Map<ReportPostDto>(reportPost);
            return reportPostDto;
        }

        public IEnumerable<ReportPostDto> GetReportedPosts(int groupId)
        {
            var reportedPosts = unitOfWork.ReportPostRepository.Find(g => g.GroupId == groupId);

            return _mapper.Map<List<ReportPostDto>>(reportedPosts);
        }
    }
}

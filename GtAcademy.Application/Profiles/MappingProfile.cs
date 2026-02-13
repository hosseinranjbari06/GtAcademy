using AutoMapper;
using GtAcademy.Application.Courses.Common;
using GtAcademy.Application.Orders.Common;
using GtAcademy.Application.Users.Common;
using GtAcademy.Domain.Courses;
using GtAcademy.Domain.Orders;
using GtAcademy.Domain.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region User

            CreateMap<User, UserSummaryDto>();

            #endregion

            #region Course

            CreateMap<CreateCourseDto, Course>();

            CreateMap<Course, CourseSummaryDto>();

            CreateMap<Course, CourseDetailsDto>();

            CreateMap<Topic, TopicDto>();

            CreateMap<Episode, EpisodeDto>();

            CreateMap<CourseComment, CourseCommentDto>();

            #endregion

            #region Order

            CreateMap<Order, OrderDetailsDto>();

            #endregion
        }
    }
}

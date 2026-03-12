using AutoMapper;
using GtAcademy.Application.Admin.Users.Commands.CreateUserByAdmin;
using GtAcademy.Application.Admin.Users.Commands.EditUserByAdmin;
using GtAcademy.Application.Admin.Users.Queries.GetUserDetailsForAdmin;
using GtAcademy.Application.Admin.Users.Queries.GetUsersListForAdmin;
using GtAcademy.Application.Courses.Common;
using GtAcademy.Application.Orders.Common;
using GtAcademy.Application.Referrals.Queries.GetUserReferralInfo;
using GtAcademy.Application.Users.Common;
using GtAcademy.Application.Users.Queries.GetUserProfile;
using GtAcademy.Domain.Courses;
using GtAcademy.Domain.Orders;
using GtAcademy.Domain.Referral;
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
            CreateMap<User, UserProfileDto>();
            CreateMap<User, EditUserProfileDto>().ReverseMap();
            CreateMap<User, UserListItemDto>();
            CreateMap<User, UserDetailsDto>();
            CreateMap<User, EditUserDto>();
            CreateMap<CreateUserDto, User>();

            #endregion

            #region Referral

            CreateMap<Referral, UsersReferredDto>();

            #endregion

            #region Course

            CreateMap<CreateCourseDto, Course>();

            CreateMap<Course, CourseSummaryDto>();

            CreateMap<Course, CourseDetailsDto>();

            CreateMap<Topic, TopicDto>();

            CreateMap<Episode, EpisodeDto>();

            CreateMap<CourseComment, CourseCommentDto>();

            CreateMap<CourseCategory, CourseCategoryDto>();

            #endregion

            #region Order

            CreateMap<Order, OrderDetailsDto>();

            #endregion
        }
    }
}

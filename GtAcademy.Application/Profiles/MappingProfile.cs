using AutoMapper;
using GtAcademy.Application.Courses.Common;
using GtAcademy.Application.Orders.Common;
using GtAcademy.Domain.Courses;
using GtAcademy.Domain.Orders;
using System;
using System.Collections.Generic;
using System.Text;

namespace GtAcademy.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            #region Course

            CreateMap<CreateCourseDto, Course>();

            CreateMap<Course, CourseSummaryDto>();

            CreateMap<Course, CourseDetailsDto>();

            #endregion

            #region

            CreateMap<Order, OrderDetailsDto>();

            #endregion
        }
    }
}

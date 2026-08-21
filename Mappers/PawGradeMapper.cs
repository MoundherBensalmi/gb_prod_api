using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using gb_prod_api.DTOs.Grade;
using gb_prod_api.Models;
using Riok.Mapperly.Abstractions;

namespace gb_prod_api.Mappers
{
    [Mapper(RequiredMappingStrategy = RequiredMappingStrategy.Target)]
    public static partial class PawGradeMapper
    {
        public static partial PawGradeResponse ToResponse(PawGrade pawGrade);

        public static partial List<PawGradeResponse> ToResponse(
            List<PawGrade> pawGrades);
    }
}
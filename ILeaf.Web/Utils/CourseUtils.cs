using ILeaf.Core.Enums;
using ILeaf.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ILeaf.Web.Utils
{
    public static class CourseUtils
    {
        public static string FormatWeeks(byte[] weeks)
        {
            StringBuilder sb = new StringBuilder();
            int currentRangeStart = 0;
            int currentRangeEnd = 0;
            int lastNumber = 0;
            int currentNumber = 0;
            for (int i = 0; i != weeks.Length; i++)
            {
                currentNumber = weeks[i];
                if (currentNumber == 0)
                {
                    if (i > 2)
                    {
                        lastNumber = weeks[i - 2];
                        currentNumber = weeks[i - 1];
                    }
                    break;
                }
                if (lastNumber != 0)
                {
                    if (currentNumber == lastNumber + 1)
                        currentRangeEnd = currentNumber;
                    else
                    {
                        if (currentRangeEnd == currentRangeStart)
                            sb.Append($"{currentRangeStart}, ");
                        else
                            sb.Append($"{currentRangeStart}~{currentRangeEnd}, ");
                        currentRangeEnd = currentNumber;
                        currentRangeStart = currentNumber;
                    }
                }
                else
                {
                    currentRangeStart = currentNumber;
                    currentRangeEnd = currentNumber;
                }

                lastNumber = currentNumber;
            }
            if (currentNumber == lastNumber + 1)
                sb.Append($"{currentRangeStart}~{currentNumber}");
            else
                sb.Append($"{currentNumber}");

            
            return sb.ToString();
        }

        public static byte[] TrimWeeks(byte[] weeks)
        {
            List<byte> ret = new List<byte>();
            foreach(byte b in weeks)
            {
                if (b == 0)
                    break;
                ret.Add(b);
            }
            return ret.ToArray();
        }

        public static string GetSemesterName(DateTime semesterStart)
        {
            int startYear = 0;
            int endYear = 0;
            string suffix = "";

            if(semesterStart.Month==2|| semesterStart.Month == 3)
            {
                startYear = semesterStart.Year - 1;
                endYear = semesterStart.Year;
                suffix = "第二学期";
            }
            else
            {
                startYear = semesterStart.Year;
                endYear = semesterStart.Year + 1;
                suffix = "第一学期";
            }

            return String.Format("{0}-{1}学年{2}", startYear, endYear, suffix);
        }

        public static string CourseChangeToString(CourseChange courseChange)
        {
            string prefix = courseChange.CourseTime.ToString("MM月DD日") + "的\"" + courseChange.Course.Title + "\"课程 ";
            string suffix = "";

            switch ((CourseChangeType)courseChange.ChangeType)
            {
                case CourseChangeType.Cancelled:
                    suffix = "已取消";
                    break;
                case CourseChangeType.ClassroomChanged:
                    suffix = "教室已更改为" + courseChange.ChangedValue;
                    break;
                case CourseChangeType.TeacherChanged:
                    suffix = "教师已更改为" + courseChange.ChangedValue;
                    break;
                case CourseChangeType.TimeModified:
                    suffix = "已更改为" + courseChange.ChangedValue + "上课";
                    break;
            }

            return prefix + suffix;
        }
    }
}
    
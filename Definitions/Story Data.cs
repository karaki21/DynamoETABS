using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DynamoETABS.Definitions;

namespace DynamoETABS.Definitions
{
    
    public class Story_Data 
    {
        //Story Names
        internal List<string> StoryNames { get; set; }
        //Story Elevations
        internal List<double> StoryElevations { get; set; }
        //Story Heights
        internal List<double> StoryHeights { get; set; }
        //Base Elevation
        internal Double BaseElevation { get; set; }

        //Creates Story Data Object
        public static Story_Data CreateStoryDataByHeights(List<double> Storyheights, List<string> Storynames, Double BaseElev = 0)
        {
            Story_Data Stories = new Story_Data(Storynames, Storyheights, BaseElev);
            //Assign Default Story Names
            //if (Storynames.Count()==0)
            //{
            //    InitializeDefStoryNames(Storyheights, ref Storynames);
            //}

            //Check Number of Story Names and StoryHeights are equal
            if (Storynames.Count() != Storyheights.Count())
            {
                string errorMessage = "Number of stories does not equal number of story heights.";
                throw new Exception(errorMessage);
            }

            //Calculate Story Elevation
            //foreach(double sh in Storyheights)
            //{

           // }

            return Stories;
        }
        public static Story_Data CreateStoryDataByElevation( List<double> Storyelevations, List<string> Storynames = null)
        {
            Story_Data Stories = new Story_Data(Storynames, Storyelevations);

            //Assign Default Story Names
            //if (Storynames.Count()==0)
            //{
            //    InitializeDefStoryNames(Storyheights, ref Storynames);
            //}

            //Check Number of Story Names and StoryHeights are equal
            if (Storynames.Count() != Storyelevations.Count())
            {
                string errorMessage = "Number of stories does not equal number of story heights.";
                throw new Exception(errorMessage);
            }

            //Calculate Story Heights
            //foreach(double sh in Storyelevations)
            //{

            // }

            return Stories;
        }


        private static void InitializeDefStoryNames (List<double> stheights,ref List<string> stnames)
        {
            int i = 0;
            foreach (double sh in stheights)
            {
                 stnames[i] = string.Concat("Story " + i);
                i++;
            }
        }
        //Story Data Constructor
        internal Story_Data() { }
        internal Story_Data(List<string> Storynames, List<double> Storyheights, Double BaseElev)
        {
            StoryNames = Storynames;
            StoryHeights = Storyheights;
            BaseElevation = BaseElev;
        }
        internal Story_Data(List<string> Storynames, List<double> Storyelevations)
        {
            StoryNames = Storynames;
            StoryElevations = Storyelevations;

        }


    }
}

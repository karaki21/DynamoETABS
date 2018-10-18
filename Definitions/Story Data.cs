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
        public static Story_Data CreateStoryDataByHeights(List<double> StoryHeights, List<string> StoryNames, Double BaseElev = 0)
        {
            Story_Data Stories = new Story_Data(StoryNames, StoryHeights, BaseElev);
            //Assign Default Story Names
            //if (Storynames.Count()==0)
            //{
            //    InitializeDefStoryNames(Storyheights, ref Storynames);
            //}

            //Check Number of Story Names and StoryHeights are equal
            if (StoryNames.Count() != StoryHeights.Count())
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
        public static Story_Data CreateStoryDataByElevation( List<double> StoryElevations, List<string> StoryNames)
        {
            Story_Data Stories = new Story_Data(StoryNames, StoryElevations);

            //Assign Default Story Names
            //if (Storynames.Count()==0)
            //{
            //    InitializeDefStoryNames(Storyheights, ref Storynames);
            //}

            //Check Number of Story Names and StoryHeights are equal
            if (StoryNames.Count() +1 != StoryElevations.Count())
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


        public static List<string> InitializeStoryNames(List<double> stheights)
        {

            List<string> stnames = new List<string>();
            foreach (double sh in stheights)
            {
                
                 stnames.Add( string.Format("Story_{0}",sh ));
                
            }
            return stnames;
        }
                    
        
        //Story Data Constructor
        internal Story_Data() { }
        internal Story_Data(List<string> Storynames, List<double> Storyheights, Double BaseElev)
        {
            StoryNames = Storynames;
            StoryHeights = Storyheights;
            BaseElevation = BaseElev;
            StoryElevations[0] = BaseElev;

            
            for (int i =1; i<StoryHeights.Count();i++)
            {
                StoryElevations[i] = StoryElevations[i - 1] + StoryHeights[i];
            }
        }
        internal Story_Data(List<string> Storynames, List<double> Storyelevations)
        {
            StoryNames = Storynames;
            StoryElevations = Storyelevations;
            
        }


    }
}

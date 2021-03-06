﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace DynamoETABS.Definitions
{
    public class Frame_Stiffness_Modifier
    {
        

        //Area
        internal double A { get; set; }
        //V2 
        internal double V2 { get; set; }
        //V3
        internal double V3 { get; set; }
        //Torsion 
        internal double T { get; set; }
        //M2
        internal double M2 { get; set; }
        //M3
        internal double M3 { get; set; }

        //define a new frame stiffness modifier object
        public static Frame_Stiffness_Modifier DefineFrameModifier( double Area = 1.0  , double V2 = 1.0 , double V3 = 1.0 , double T = 1.0 , double M2 = 1.0 , double M3 = 1.0)
        {

            Frame_Stiffness_Modifier FMod = new Frame_Stiffness_Modifier( Area , V2 , V3 , T , M2 , M3 );
           
            return FMod;

        }

        //constructors
        internal Frame_Stiffness_Modifier(double a, double v2, double v3, double t, double m2, double m3)
        {
            A = a;
            V2 = v2;
            V3 = v3;
            T = t;
            M2 = m2;
            M3 = m3;
        }

        internal Frame_Stiffness_Modifier()
        {
        }
    }


    public class Shell_Stiffness_Modifier
    {
        


        //Membrane f11
        internal double F11 { get; set; }
        //Membrane f22
        internal double F22 { get; set; }
        //Membrane f12
        internal double F12 { get; set; }
        //Bending m11 
        internal double M11 { get; set; }
        //Bending m22
        internal double M22 { get; set; }
        //Bending m12
        internal double M12 { get; set; }


        //define a new shell stiffness modifier object
        public static Shell_Stiffness_Modifier DefineShellModifier(double Membrane_f11, double Membrane_f22, double Membrane_f12, double Bending_m11, double Bending_m22, double Bending_m12)
        {

            Shell_Stiffness_Modifier SMod = new Shell_Stiffness_Modifier( Membrane_f11 , Membrane_f22 , Membrane_f12 , Bending_m11 , Bending_m22 , Bending_m12 );
           
            return SMod;

        }


        //Constructors
        internal Shell_Stiffness_Modifier(double f11, double f22, double f12, double m11, double m22, double m12)
        {
            F11 = f11;
            F22 = f22;
            F12 = f12;
            M11 = m11;
            M22 = m22;
            M12 = m12;
        }

        internal Shell_Stiffness_Modifier()
        {
        }
    }

}

# tomogram_visualizer
// от синего до желтого 
//(1-x)A+x*B x - wdtn 
//x цвет тизменение от 0 до 1 в трансфере
            int min = Form1.minBar;
            int max = Form1.widthBar;
            int blue, yellow;
            //int newVal = Clamp((value - min) * 255 / (max - min), 0, 255);
            //return Color.FromArgb(255, newVal, newVal, newVal);
            int newVal = Clamp((value - min) / (max - min), 0, 1);

            blue = (1 - newVal) * 255;
            yellow = newVal*255;
            //Clamp(blue, 0, 255);
            return Color.FromArgb(255, yellow, yellow, blue);

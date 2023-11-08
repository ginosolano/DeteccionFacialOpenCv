using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using System.IO;

namespace PruebaOpenCv
{
    class Program
    { 
        static void Main(string[] args)
        {
            string directorioActual = Directory.GetCurrentDirectory();  //Se crea un string con la ruta actual para luego poder usar una ruta relativa. Crear la ruta relativa nos va a permitir ejecutar correctamente la aplicacion sin necesitar la ruta absoluta.

            string rutaVideo = Path.Combine(directorioActual, "cuenta.mp4"); //Se utiliza la ruta "directorioActual" para crear la ruta relativa del video "cuenta.mp4"
            string rutaFrames = Path.Combine(directorioActual, "frames"); //Se utiliza la ruta "directorioActual" para crear la ruta relativa de la carpeta "frames"

            VideoCapture capture = new VideoCapture(rutaVideo); //Creamos el objeto capture con el video "cuenta.mp4", utilizando la ruta "rutaVideo"
            Mat frame = new Mat();                                  //Creamos el objeto frame vacio.
            {
                int frameNumber = 0;                                //Inicializamos el nro del frame en 0, para luego nombrar los frames.
                
                while(capture.Read(frame))                          //cada frame que tome del video lo guarda en "frame" mientras capture tenga frames.
                {
                    string frameFileName = $"{rutaFrames}\\frame_{frameNumber:D4}.jpg"; //Se crea el string que será el nombre de cada frame y le damos la dirección de la carpeta en la que vamos a guardar los frames.
                    Cv2.ImWrite(frameFileName, frame);                  //teniendo nombre del frame y el frame, se guarda en la carpeta "frames" en formato .jpg
                    frameNumber++;                                       //incrementamos en 1 el numero de frame "frameNumber"

                
                }
                capture.Dispose();                              //Liberamos los recursos del objeto capture
            }
        }
    }
}

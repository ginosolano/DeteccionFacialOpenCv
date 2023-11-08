using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;
using System.IO;
using OpenCvSharp.Dnn;
using OpenCvSharp.Extensions;


namespace DiferenciaFacial
{
    class Program
    {
        static void Main(string[] args)
        {
            string directorioActual = Directory.GetCurrentDirectory();

            string rutaVideo = Path.Combine(directorioActual, "videoprueba.mp4");                           //Creacion de rutas que nos servirán a lo largo del programa
            string rutaFrames = Path.Combine(directorioActual, "framesFaciales");
            string rutaHaarcascade = Path.Combine(directorioActual, "haarcascade_frontalface_default.xml");

            CascadeClassifier faceCascade = new CascadeClassifier(rutaHaarcascade);                     //crea un objeto CascadeClassifier para la detección de rostros.

            VideoCapture capture = new VideoCapture(rutaVideo);
            Mat Frame = new Mat();
            {
                int frameNumber = 0;                                //Inicializamos el nro del frame en 0, para luego nombrar los frames.

                while (capture.Read(Frame))                          //cada frame que tome del video lo guarda en "frame" mientras capture tenga frames.
                {
                    Mat grayImage = new Mat();                      
                    Cv2.CvtColor(Frame, grayImage, ColorConversionCodes.BGR2GRAY);      //Se crea una imagen en escala de grises a partir del frame  "Frame".

                    Rect[] faces = faceCascade.DetectMultiScale(                      //Detecta rostros en la imagen en escala de grises  
                        grayImage,
                        scaleFactor: 1.1,
                        minNeighbors: 5,
                        flags: HaarDetectionTypes.DoRoughSearch,
                        minSize: new Size(30,30)
                        );
                
                    foreach(Rect face in faces)
                    {
                        Cv2.Rectangle(Frame, face, Scalar.Red, 2); //En esta parte se dibuja los rectangulos al rededor de los rostros
                    }
                    string frameFileName = $"{rutaFrames}\\frame_{frameNumber:D4}.jpg"; //Se crea el string que será el nombre de cada frame y le damos la dirección de la carpeta en la que vamos a guardar los frames.
                    Cv2.ImWrite(frameFileName, Frame);                  //teniendo nombre del frame y el frame, se guarda en la carpeta "frames" en formato .jpg
                    frameNumber++;
                }


            }
        }
    }
}

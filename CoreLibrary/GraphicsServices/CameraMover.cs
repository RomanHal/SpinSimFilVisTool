using CoreLibrary.GraphicsServices.Interfaces;
using CoreLibrary.Interfaces;
using GraphicLibrary.Management;
using System;
using System.Collections.Generic;
using System.Text;

namespace CoreLibrary.GraphicsServices
{
    public class CameraMover : ICameraMover
    {
        public void LookAt(IObject @object)
        {
            if(@object is SpintronicsObject)
            {
                var atlas = @object as SpintronicsObject;
                double x=(atlas.GraphicsObject.FirstPoint.X+ atlas.GraphicsObject.LastPoint.X)/ 2;
                double y=(atlas.GraphicsObject.FirstPoint.Y+ atlas.GraphicsObject.LastPoint.Y)/ 2;
                double z= (atlas.GraphicsObject.FirstPoint.Z+ atlas.GraphicsObject.LastPoint.Z)/ 2;
                GraphicManagement.cameraCommands.MoveTo(x, y, z);
            }
        }

        public void LookAt(IEnergy energy,int index=0)
        {
            if(energy is SpintronicsEnergy)
            {
                var spintronicsEnergy = energy as SpintronicsEnergy;
                if(spintronicsEnergy.GraphicsObjects.Count>0)
                {
                    double x = (spintronicsEnergy.GraphicsObjects[index].FirstPoint.X +
                        spintronicsEnergy.GraphicsObjects[index].LastPoint.X) / 2;
                    double y = (spintronicsEnergy.GraphicsObjects[index].FirstPoint.Y +
                        spintronicsEnergy.GraphicsObjects[index].LastPoint.Y) / 2;
                    double z = (spintronicsEnergy.GraphicsObjects[index].FirstPoint.Z +
                        spintronicsEnergy.GraphicsObjects[index].LastPoint.Z) / 2;
                    GraphicManagement.cameraCommands.MoveTo(x, y, z);
                }
            }
        }
    }
}

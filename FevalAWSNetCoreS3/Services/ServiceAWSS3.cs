﻿using Amazon.S3;
using Amazon.S3.Model;

namespace FevalAWSNetCoreS3.Services
{
    public class ServiceAWSS3
    {
        //NECESITAMOS EL NOMBRE DE NUESTRO BUCKET
        private string BucketName;

        //TAMBIEN NECESITAMOS UN CLIENTE DE Amazon S3
        private IAmazonS3 ClienteS3;

        //EL CLIENTE NUNCA SE PONE DE FORMA EXPLICITA, ES 
        //LA ARQUITECTURA NET CORE QUIEN NOS DARA EL CLIENTE 
        //CONFIGURADO.  RECIBIMOS UN CLIENTE DE AMAZON
        public ServiceAWSS3(IAmazonS3 amazonS3)
        {
            this.BucketName = "bucket-back-paco";
            this.ClienteS3 = amazonS3;
        }

        //COMENZAMOS SUBIENDO FICHEROS AL BUCKET
        public async Task<bool>
            UploadFileAsync(string fileName, Stream stream)
        {
            //LOS SERVICIOS DENTRO DE AWS SON CASI TODOS MUY PARECIDOS
            //TODOS FUNCIONAN MEDIANTE PETICIONES WEB, ENVIANDO PARAMETROS
            PutObjectRequest request = new PutObjectRequest
            {
                InputStream = stream,
                Key = fileName,
                BucketName = this.BucketName
            };
            //PARA EJECUTAR LA ACCION, DEBEMOS RECIBIR UNA RESPUESTA
            PutObjectResponse response =
                await this.ClienteS3.PutObjectAsync(request);
            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

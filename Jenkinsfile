pipeline {
    agent any

    environment {
        // Nombre de la imagen que crearemos
        IMAGE_NAME = "toothy-api"
    }

    stages {
        stage('Checkout') {
            steps {
                // Descarga el código del repo
                checkout scm
            }
        }

        stage('Build .NET') {
            steps {
                // Compila la solución completa
                echo 'Compilando la solución...'
                sh 'dotnet build DentalClinicApp.sln --configuration Release'
            }
        }

        stage('Unit Tests') {
            steps {
                // Ejecuta las pruebas. Si fallan, el pipeline se detiene.
                echo 'Ejecutando pruebas unitarias...'
                // Ajusta la ruta si tu carpeta de tests tiene otro nombre
                sh 'dotnet test tests/Toothy.UnitTests/Toothy.UnitTests.csproj --no-build --verbosity normal'
            }
        }

        stage('Docker Build') {
            steps {
                echo 'Creando imagen Docker...'
                // Construye la imagen usando el Dockerfile de la raíz
                // Nota: El punto '.' al final es vital (es el contexto)
                sh "docker build -t ${IMAGE_NAME}:latest -f Dockerfile ."
            }
        }
    }
    
    post {
        always {
            echo 'Pipeline finalizado.'
        }
        success {
            echo '¡Éxito! El código pasó todas las pruebas y se creó la imagen.'
        }
        failure {
            echo 'Ups, algo falló. Revisa los logs.'
        }
    }
}
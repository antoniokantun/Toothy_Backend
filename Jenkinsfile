pipeline {
    agent any 

    environment {
        IMAGE_NAME = "toothy-api"
    }

    stages {
        stage('Checkout') {
            steps {
                checkout scm
            }
        }

        // --- AQUÍ ESTÁ LA MAGIA ---
        // Le decimos a Jenkins: "Para estos pasos, usa un contenedor de .NET 10"
        stage('Compile & Test (.NET)') {
            agent {
                docker { 
                    // Usamos la imagen oficial de Microsoft SDK
                    image 'mcr.microsoft.com/dotnet/sdk:10.0' 
                    // Esto asegura que use el mismo espacio de trabajo
                    reuseNode true 
                }
            }
            steps {
                echo '--- Compilando dentro del contenedor .NET ---'
                sh 'dotnet --version' // Solo para verificar que funciona
                sh 'dotnet build Toothy.sln --configuration Release'
                
                echo '--- Ejecutando Tests dentro del contenedor .NET ---'
                // Ejecutamos las pruebas
                sh 'dotnet test tests/Toothy.UnitTests/Toothy.UnitTests.csproj --no-build --verbosity normal'
            }
        }

        // Esta etapa vuelve a usar el agente "any" (Tu Jenkins con Docker socket)
        stage('Build Docker Image') {
            steps {
                echo '--- Creando la imagen final de Docker ---'
                // Nota: Aquí se usa el Dockerfile para empaquetar
                sh "docker build -t ${IMAGE_NAME}:latest -f Dockerfile ."
            }
        }
    }
    
    post {
        always {
            echo 'Pipeline finalizado.'
        }
        failure {
            echo '❌ Error en el Pipeline.'
        }
        success {
            echo '✅ ¡Éxito! Todo funciona.'
        }
    }
}

pipeline {
  agent any
  environment {
    VERSION_PREFIX = "0.1."
    VERSION_NUMBER = "${VERSION_PREFIX}${currentBuild.getNumber()}"
    PACKAGE_NAME = "OrdenIEgetHus"
    OCTOPUS_API_KEY = credentials('arkitektum-octopus-api-key')
  }
  stages {
    stage('Build') {
      steps {
        bat "dotnet build --configuration \"Release\" /property:Version=${VERSION_NUMBER}"
      }
    }
    stage('Test') {
      steps {
        bat 'dotnet test Arkitektum.Orden.Test'
      }
    }
    stage('dotnet publish') {
        steps {
        bat 'dotnet publish --configuration "Release" Arkitektum.Orden/Arkitektum.Orden.csproj  --output output-app'
        }
    }
    stage('octopus pack and publish') {
        steps {
          dir("Arkitektum.Orden\\output-app") {
            bat "octo pack --id ${PACKAGE_NAME} --version ${VERSION_NUMBER}"
            bat "octo push --package ${PACKAGE_NAME}.${VERSION_NUMBER}.nupkg --replace-existing --server http://localhost:8081 --apiKey ${OCTOPUS_API_KEY}"
          }
      }
    }
  }
}

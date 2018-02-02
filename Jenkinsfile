pipeline {
  agent any
  environment {
    VERSION_SUFFIX = "0.1."
    VERSION_NUMBER = "${VERSION_SUFFIX} ${currentBuild.getNumber()}"
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
    stage('Publish') {
        steps {
        bat 'dotnet publish --configuration "Release" Arkitektum.Orden/Arkitektum.Orden.csproj  --output output-app'
        bat 'octo pack --id OrdenIEgetHus --version ${VERSION_NUMBER} --basePath output-app'
      }
    }
  }
}

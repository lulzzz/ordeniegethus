pipeline {
  agent any
  stages {
    stage('Build') {
      steps {
        bat 'dotnet build --configuration "Release" /property:Version=${env.BUILD_ID}'
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
        bat 'octo pack --id OrdenIEgetHus --version ${env.BUILD_NUMBER} --basePath output-app'
      }
    }
  }
}

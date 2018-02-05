pipeline {
  agent any
  environment {
    VERSION_PREFIX = "0.1."
    VERSION_NUMBER = "${VERSION_PREFIX}${currentBuild.getNumber()}"
    PACKAGE_NAME = "OrdenIEgetHus"
    OCTOPUS_API_KEY = credentials('arkitektum-octopus-api-key')
    OCTOPUS_PROJECT_NAME = "Orden i eget hus"
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
    stage('octopus relase and deploy') {
      steps {
        bat "octo create-release --project \"${OCTOPUS_PROJECT_NAME}\" --version ${VERSION_NUMBER} --packageversion ${VERSION_NUMBER} --server http://localhost:8081/ --apiKey ${OCTOPUS_API_KEY} --releaseNotes \"Jenkins build [${VERSION_NUMBER}](https://ci.arkitektum.no/blue/organizations/jenkins/ordeniegethus/detail/master/${currentBuild.getNumber()}/changes/)\" --deployto=Dev --progress"
      }
    }
  }
  post {
    always {
      dir("Arkitektum.Orden\\output-app") {
        deleteDir()
      }
    }
  }
}

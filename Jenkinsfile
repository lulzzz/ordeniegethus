pipeline {
  agent any
  environment {
    VERSION_PREFIX = "0.3."
    VERSION_NUMBER = "${VERSION_PREFIX}${currentBuild.getNumber()}"
    PACKAGE_NAME = "OrdenIEgetHus"
    OCTOPUS_API_KEY = credentials('arkitektum-octopus-api-key')
    OCTOPUS_PROJECT_NAME = "Orden i eget hus"
  }
  stages {
    stage('Build') {
      steps {
        dir("Arkitektum.Orden") {
          bat 'yarn'
          bat 'node node_modules/webpack/bin/webpack.js'
        }
        bat "dotnet build --configuration \"Release\" /property:Version=${VERSION_NUMBER}"
      }
    }
    stage('Test') {
      steps {
        bat 'dotnet test Arkitektum.Orden.Test'
      }
    }
    stage('Build package') {
      when {
        anyOf {
          branch 'master'
          branch 'hj_azure-logging'
        }
      }
      steps {
        bat 'dotnet publish --configuration "Release" Arkitektum.Orden/Arkitektum.Orden.csproj  --output output-app'
        dir("Arkitektum.Orden\\output-app") {
          bat "octo pack --id ${PACKAGE_NAME}-${BRANCH_NAME} --version ${VERSION_NUMBER}"
          bat "octo push --package ${PACKAGE_NAME}-${BRANCH_NAME}.${VERSION_NUMBER}.nupkg --replace-existing --server http://localhost:8081 --apiKey ${OCTOPUS_API_KEY}"
        }
      }
    }
    stage('Deploy') {
      when {
        branch 'master'
      }
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
      dir("Arkitektum.Orden\\bin") {
        deleteDir()
      }
      dir("Arkitektum.Orden.Test\\bin") {
        deleteDir()
      }
    }
    failure {
      slackSend(channel: '#feed-ordeniegethus',
        color: 'danger', 
        message: "Build failed: '${env.JOB_NAME} [${env.BUILD_NUMBER}]' (${env.BUILD_URL})")
    }
    changed {
      slackSend(channel: '#feed-ordeniegethus',
        color: 'warning',
        message: "Build state has changed: '${currentBuild.result} ${env.JOB_NAME} [${env.BUILD_NUMBER}]' (${env.BUILD_URL})")
    }
  }
}

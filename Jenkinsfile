@Library('CraftingIT-Library') _

pipeline {
	agent {
		label 'DOCKER'
	}
    stages{
        stage('Build') {
            steps {
                script {
                    def app = docker.build("craftingit/gitrelease")
                }
            }
        }
        stage('Release') {
            when { buildingTag() }
            steps {
                echo env.TAG_NAME
            }
        }
    }
    post {
        always {
            step ([$class: 'WsCleanup'])
            script { 
                mailHelper.notifyEmail()
            }
        } 
    }
}

@Library('CraftingIT-Library') _

pipeline {
	agent {
		label 'DOTNET'
	}

	stages{
        stage('Dependencys') {
            steps {
				script {
                    if (isUnix()){
                        sh 'dotnet restore'
                    } else {
                        bat 'dotnet restore'
                    }
                }
            }
        }
		stage('Build') {
            steps {
				script {
                    if (isUnix()){
                        sh 'dotnet build'
                    } else {
                        bat 'dotnet build'
                    }
					parsingHelper.parseTodosAll()
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

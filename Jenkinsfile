@Library('CraftingIT-Library') _

pipeline {
	agent {
		label 'DOTNET'
	}
    stages{
        stage('Build') {
            steps {
                script {
                    if (isUnix()){
                        sh 'dotnet restore' 
                        sh 'dotnet build' 
                    } else {
                        bat 'dotnet restore' 
                        bat 'dotnet build' 	
                    }
                }
            }
        }
        stage('Release') {
            when { buildingTag() }

            parallel {
                stage('Local') {
                    environment {
                        PROJECT_NAME = release.getRepoName()
                        REPO_OWNER_NAME = release.getRepoOwner()
                    }
                    steps {
                        echo env.TAG_NAME
                        //Bauen
                        script {
                            if (isUnix()){
                                sh 'dotnet publish -o outWin -r win-x64 -c Release -p:PublishSingleFile=true -p:PublishTrimmed=true -p:Version=$TAG_NAME -p:AssemblyVersion=$TAG_NAME $PROJECT_NAME.csproj'
                                sh 'dotnet publish -o outLin -r linux-x64 -c Release -p:PublishSingleFile=true -p:PublishTrimmed=true -p:Version=$TAG_NAME -p:AssemblyVersion=$TAG_NAME $PROJECT_NAME.csproj'
                                sh 'dotnet publish -o outOsx -r osx-x64 -c Release -p:PublishSingleFile=true -p:PublishTrimmed=true -p:Version=$TAG_NAME -p:AssemblyVersion=$TAG_NAME $PROJECT_NAME.csproj'
                            } else {
                                bat 'dotnet publish -o outWin -r win-x64 -c Release -p:PublishSingleFile=true -p:PublishTrimmed=true -p:Version=%TAG_NAME% -p:AssemblyVersion=%TAG_NAME% %PROJECT_NAME%.csproj'
                                bat 'dotnet publish -o outLin -r linux-x64 -c Release -p:PublishSingleFile=true -p:PublishTrimmed=true -p:Version=%TAG_NAME% -p:AssemblyVersion=%TAG_NAME% %PROJECT_NAME%.csproj'
                                bat 'dotnet publish -o outOsx -r osx-x64 -c Release -p:PublishSingleFile=true -p:PublishTrimmed=true -p:Version=%TAG_NAME% -p:AssemblyVersion=%TAG_NAME% %PROJECT_NAME%.csproj'
                            }
                        }
                        //Hochladen
                        withCredentials([string(credentialsId: 'GitHub-KST', variable: 'TOKEN')]) {
                            
                            script {
                                if (isUnix()){
                                    sh './outLin/gitRelease github upload --tag $TAG_NAME --ApiToken $TOKEN --owner $REPO_OWNER_NAME --repo $PROJECT_NAME --filename ./outWin/gitRelease.exe'
                                    sh './outLin/gitRelease github upload --tag $TAG_NAME --ApiToken $TOKEN --owner $REPO_OWNER_NAME --repo $PROJECT_NAME --filename ./outLin/gitRelease'
                                    sh './outLin/gitRelease github upload --tag $TAG_NAME --ApiToken $TOKEN --owner $REPO_OWNER_NAME --repo $PROJECT_NAME --filename ./outOsx/gitRelease'
                                } else {
                                    bat '.\\outWin\\gitRelease.exe github upload --tag %TAG_NAME% --ApiToken %TOKEN% --owner %REPO_OWNER_NAME% --repo %PROJECT_NAME% --filename .\\outWin\\gitRelease.exe'
                                    bat '.\\outWin\\gitRelease.exe github upload --tag %TAG_NAME% --ApiToken %TOKEN% --owner %REPO_OWNER_NAME% --repo %PROJECT_NAME% --filename .\\outLin\\gitRelease'
                                    bat '.\\outWin\\gitRelease.exe github upload --tag %TAG_NAME% --ApiToken %TOKEN% --owner %REPO_OWNER_NAME% --repo %PROJECT_NAME% --filename .\\outOsx\\gitRelease'
                                }
                            }
                        }
                    }
                }
                stage('Docker') {
                    agent {
                        label "DOCKER"
                    }
                    stages {
                        stage('Build Release') {
                            steps {           
                                script {
                                    def app = docker.build("craftingit/gitrelease")
                                    def version = env.TAG_NAME;

                                    docker.withRegistry('', 'CraftingIT-Bot_Dockerhub') {
                                        app.push("latest")
                                        app.push(version)
                                    }
                                }
                            } 
                        }
                    }
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

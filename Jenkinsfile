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

                        zip zipFile: 'win-x64_gitRelease.zip', archive: false, dir: './outWin/gitRelease.exe'
                        zip zipFile: 'linux-x64_gitRelease.zip', archive: false, dir: './outLin/gitRelease.exe'
                        zip zipFile: 'osx-x64_gitRelease.zip', archive: false, dir: './outOsx/gitRelease.exe'

                        //Hochladen
                        withCredentials([string(credentialsId: '66cf66bd-888b-489e-8fd8-10026e30e1e6', variable: 'TOKEN')]) {         
                            script {
                                if (isUnix()){
                                    sh './outLin/gitRelease github upload --tag $TAG_NAME --ApiToken $TOKEN --owner $REPO_OWNER_NAME --repo $PROJECT_NAME --filename win-x64_gitRelease.zip'
                                    sh './outLin/gitRelease github upload --tag $TAG_NAME --ApiToken $TOKEN --owner $REPO_OWNER_NAME --repo $PROJECT_NAME --filename linux-x64_gitRelease.zip'
                                    sh './outLin/gitRelease github upload --tag $TAG_NAME --ApiToken $TOKEN --owner $REPO_OWNER_NAME --repo $PROJECT_NAME --filename osx-x64_gitRelease.zip'
                                } else {
                                    bat '.\\outWin\\gitRelease.exe github upload --tag %TAG_NAME% --ApiToken %TOKEN% --owner %REPO_OWNER_NAME% --repo %PROJECT_NAME% --filename win-x64_gitRelease.zip'
                                    bat '.\\outWin\\gitRelease.exe github upload --tag %TAG_NAME% --ApiToken %TOKEN% --owner %REPO_OWNER_NAME% --repo %PROJECT_NAME% --filename linux-x64_gitRelease.zip'
                                    bat '.\\outWin\\gitRelease.exe github upload --tag %TAG_NAME% --ApiToken %TOKEN% --owner %REPO_OWNER_NAME% --repo %PROJECT_NAME% --filename osx-x64_gitRelease.zip'
                                }
                            }
                        }

                        // Nuget
                        withCredentials([string(credentialsId: 'CraftingIT-Nuget', variable: 'TOKEN')]) {         
                            script {
                                if (isUnix()){
                                    sh 'dotnet pack --configuration Release -p:PackageVersion=$TAG_NAME -p:Version=$TAG_NAME -p:AssemblyVersion=$TAG_NAME -p:SymbolPackageFormat=snupkg $PROJECT_NAME.csproj'
                                    sh 'dotnet nuget push ./nupkg/gitRelease.*.nupkg -s https://www.nuget.org/api/v2/package -k $TOKEN --skip-duplicate'
                                 } else {
                                    bat 'dotnet pack --configuration Release -p:PackageVersion=%TAG_NAME% -p:Version=%TAG_NAME% -p:AssemblyVersion=%TAG_NAME% -p:SymbolPackageFormat=snupkg %PROJECT_NAM%.csproj'
                                    bat 'dotnet nuget push .\\nupkg\\gitRelease.*.nupkg -s https://www.nuget.org/api/v2/package -k %TOKEN% --skip-duplicate'
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

pipeline {
	agent {
		label 'master'
	}

	stages {
		stage('Build') {
			steps {
				bat """
					nuget restore

					CALL bin\\msbuild_where.cmd \"Jenkins.NET.sln\" /m ^
						/p:Configuration=Release ^
						/p:Platform=\"Any CPU\" ^
						/target:Build
				"""
			}
		}
		stage('Deploy') {
			steps {
				bat "CALL bin\\Publish.cmd"
			}
		}
	}
}

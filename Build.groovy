pipeline {
	agent {
		label 'master'
	}

	stages {
		stage('Build') {
			steps {
				bat """
					nuget restore
					CALL bin\\msbuild_where.cmd \"Jenkins.NET.sln\" /p:Configuration=Release /p:Platform=\"Any CPU\" /m
				"""
			}
		}
	}
}

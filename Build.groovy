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
						/p:Configuration=\"Release\" ^
						/p:Platform=\"Any CPU\" ^
						/target:Build
				"""
			}
		}
		stage('Test') {
			steps {
				bat """
					nunit3-console \"Jenkins.NET.Tests\\bin\\Release\\Jenkins.NET.Tests.dll\" ^
						--result=\"Jenkins.NET.Tests\\bin\\Release\\TestResults.xml\" ^
						--where=\"cat == 'unit'\"
				"""
			}
			post {
				always {
					archiveArtifacts artifacts: "Jenkins.NET.Tests\\bin\\Release\\TestResults.xml"

					step([$class: 'NUnitPublisher',
						testResultsPattern: "Jenkins.NET.Tests\\bin\\Release\\TestResults.xml",
						keepJUnitReports: true,
						skipJUnitArchiver: false,
						failIfNoResults: true,
						debug: false])
				}
			}
		}
	}
}

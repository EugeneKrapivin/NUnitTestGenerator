$testProjectName = @(
	"NUnitTestGenerator.Tests"
)

$testProjectName | ForEach-Object -Parallel {
	Write-Host "tests/${_}/${_}.csproj"
	dotnet test `
	tests/$_/$_.csproj `
	-c Release `
	-l "console;verbosity=normal" `
	/p:CollectCoverage=true `
	/p:CoverletOutputFormat=cobertura `
	/p:CoverletOutput=../../coverlet/$_.coverage.xml
}

ReportGenerator "-targetdir:coverlet\result" "-reports:coverlet\*.xml" "-reporttypes:TeamCitySummary;HTML"

Write-Host "##teamcity[publishArtifacts 'coverlet\result'=> results]"
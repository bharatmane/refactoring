# Define the replacement items with old and new assembly references
$replacementItems = @(
    @{
        Old = 'Infragistics2.Excel.v6.3.dll</HintPath>'
        New = '<HintPath>newPath...'
    },
    @{
        Old = 'Infragistics2.Shared.v6.3.dll</HintPath>'
        New = '<HintPath>newPath...'
    },
    @{
        Old = 'Infragistics2.Win.UltraWinDataSource.v6.3.dll</HintPath>'
        New = '<HintPath>newPath...'
    }
)

# Get all project files (*.csproj) recursively from the specified path
Get-ChildItem -Path "solutionFolder\" -Filter *.csproj -Recurse | ForEach-Object {
    try {
        # Log the start of processing for each file
        Write-Host "Processing file: $($_.FullName)"
        $content = Get-Content $_.FullName
        $newContent = @()

        # Iterate through each line in the file content
        foreach ($line in $content) {
            $replacedLine = $line

            # Iterate through the list of replacement items
            foreach ($item in $replacementItems) {
                if ($line -like "*$($item.Old)*") {
                    # Log the replacement action
                    Write-Host "Replacing $($item.Old) with $($item.New)"
                    $replacedLine = $item.New
                    break # Exit the loop after the first match to avoid multiple replacements
                }
            }

            # Append the modified or unmodified line to the new content
            $newContent += $replacedLine
        }

        # Write the updated content back to the file
        Set-Content -Path $_.FullName -Value $newContent
        Write-Host "Finished processing file: $($_.FullName)"

    } catch {
        # Log any errors that occur during processing
        Write-Host "Error processing file: $($_.FullName). Error: $_"
    }
}

Write-Host "Script execution completed."

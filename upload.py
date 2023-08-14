from os import system as s

print('Adding files')
s('git add .')

cn = input('Commit description: ')
print('Committing')
s(f'git commit -m "{cn}"')

print('Pushing to GitHub')
s('git push -f origin main')

print('Finished!')
s('pause')
module.exports = function(grunt) {
  require('load-grunt-tasks')(grunt);
  require('time-grunt')(grunt);

  grunt.initConfig({
  
    pkg: grunt.file.readJSON('package.json'),
  
    nugetpush: {
        dist: {
            src: 'pub/<%= pkg.name %>.<%= pkg.version %>.0.nupkg'
        }
    },
    
    shell: {
        nugetpack: {
            options: {
                stdout: true
            },
            command: 'md pub & nuget pack Lib/OfxSharp.csproj -Prop Configuration=Release -OutputDirectory pub'
        },
        gitcommit: {
            options: {
                stdout: true
            },
            command: 'git commit -a -m "version <%= pkg.version %>'
        }
    },
    
    assemblyinfo: {
        options: {
            files: ['Lib/OfxSharp.csproj'],
            info: {
                description: '<%= pkg.description %>', 
                configuration: 'Release', 
                company: '<%= pkg.author %>', 
                product: '<%= pkg.name %>', 
                copyright: 'Copyright © <%= pkg.author %> ' + (new Date().getYear() + 1900), 
                version: '<%= pkg.version %>.0', 
                fileVersion: '<%= pkg.version %>.0'
            }
        }
    },
    
    msbuild: {
        src: ['OfxSharp.sln'],
        options: {
            verbosity: 'minimal',
            projectConfiguration: 'Release',
            targets: ['Clean', 'Rebuild'],
            stdout: true
        }
    },
    
    nunit: {
        options: {
            files: ['Tests/bin/Release/OfxSharp.Tests.dll']
        }
    }
    
  });
  grunt.registerTask('default', ['assemblyinfo', 'msbuild', 'nunit', 'shell:nugetpack']);
  grunt.registerTask('push', ['default', 'nugetpush', 'shell:gitcommit']);
};
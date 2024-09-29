import { defineConfig, UserConfig } from "vite";
import { outputPath, outputVersion } from './config.outputPath.js';
import tsconfigPaths from "vite-tsconfig-paths";

const buildFiles = [
  "src/uelp-umbraco-external-login-provider.element.ts"
]

const isDebugMode = outputPath.includes("Debug");

if (isDebugMode){
  console.log("Debug mode is enabled - console.log and debuggers are active!")
}

export let config: UserConfig =
{
  build: {
    target: 'modules',
    lib: {
      entry: buildFiles,
        formats: ["es"],
    },
    outDir: outputPath,
    emptyOutDir: true,
    sourcemap: false,
    rollupOptions: {
      external: [/^@umbraco/],
      onwarn: () => {},
      output: {
        banner: `/*! Umbraco External Login Provider v${outputVersion} ¡*/`,
        chunkFileNames: (chunkInfo) =>{
          //console.log(JSON.stringify(chunkInfo));
          return `${chunkInfo.name}.js`

        },
      }
    },
      copyPublicDir: true
  },
  esbuild: {
    drop: isDebugMode ? [] : ['console', 'debugger']
  },
  plugins: [tsconfigPaths()],
};

export default defineConfig(config)

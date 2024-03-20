import { defineConfig } from 'vite';
import path from 'path';
import { glob } from 'glob';

const staticFiles = glob.sync("./src/static/**/*.{css,js}", { dotRelative: true }).map((v) => path.resolve(__dirname, v));

export default defineConfig({
    build: {
        cssCodeSplit: true,
        lib: {
            entry: staticFiles,
            formats: ['es']
        },
        rollupOptions: {
            output: {
                preserveModules: true,
                preserveModulesRoot: path.resolve(__dirname, "./src/static"),
            }
        },
    },
    root: "./src",
    resolve: {
        alias: [
            { find: "$static", replacement: path.resolve(__dirname, 'src', 'static') },
        ]
    },
    // https://vitejs.dev/config/server-options#server-origin
    server: {
        origin: 'http://localhost:5173',
    }
})
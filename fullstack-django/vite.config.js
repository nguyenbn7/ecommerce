import { defineConfig } from 'vite';
import path from 'path';

export default defineConfig({
    build: {
        manifest: true
    },
    base: process.env === "production" ? "/static" : "/",
    root: "./src",
    resolve: {
        alias: [
            { find: "$static", replacement: path.resolve(__dirname, 'src', 'static') },
            { find: "$public", replacement: path.resolve(__dirname, 'src', 'public') }
        ]
    }
})
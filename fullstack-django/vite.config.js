import { defineConfig } from 'vite';
import path from 'path';

export default defineConfig({
    build: {
        manifest: true
    },
    base: process.env.NODE_ENV === "production" ? "/static" : "/",
    root: "./src",
    resolve: {
        alias: [
            { find: "$static", replacement: path.resolve(__dirname, 'src', 'static') },
        ]
    }
})
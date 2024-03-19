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
    },
    // https://vitejs.dev/config/server-options#server-origin
    server: {
        origin: 'http://localhost:5173',
    }
})
const express = require("express");
const https = require("https");
const fs = require("fs");
const bodyParser = require("body-parser");
const axios = require("axios");
const {
    default: Axios
} = require("axios");

const PORT = process.env.PORT;
const PASS_PHRASE = process.env.SSL_PP;
const CALLBACK_URI = "io.devmikan.lyrico://callback";
const SPOTIFY_CLIENT_SECRET = process.env.SPOTIFY_CLIENT_SECRET;
const SPOTIFY_CLIENT_ID = process.env.SPOTIFY_CLIENT_ID;
if (!SPOTIFY_CLIENT_SECRET || !SPOTIFY_CLIENT_ID) {
    console.log("The SPOTIFY_CLIENT_SECRET and/or SPOTIFY_CLIENT_ID environment variable is not defined!")
    process.exit(1);
}

const app = express();
app.use(bodyParser.urlencoded({
    extended: true
}));

function log(id, msg) {
    var today = new Date();
    var time = today.getHours() + ":" + today.getMinutes() + ":" + today.getSeconds();
    console.log("[" + time + "]: " + "<" + id + "> " + msg);
}

app.post("/swap", async (req, res) => {
    const {
        code
    } = req.body;

    const params = new URLSearchParams();
    params.append("grant_type", "authorization_code");
    params.append("code", code);
    params.append("redirect_uri", CALLBACK_URI);
    params.append("client_secret", SPOTIFY_CLIENT_SECRET)
    params.append("client_id", SPOTIFY_CLIENT_ID);

    try {
        const {
            data
        } = await Axios.post("https://accounts.spotify.com/api/token", params);

        log("swap", "Success!");
        return res.send(data);
    } catch (ex) {
        log("error", ex);
    }
});

app.post("/refresh", async (req, res) => {
    const {
        refresh_token
    } = req.body;

    const params = new URLSearchParams();
    params.append("grant_type", "refresh_token");
    params.append("refresh_token", refresh_token);
    params.append("client_secret", SPOTIFY_CLIENT_SECRET)
    params.append("client_id", SPOTIFY_CLIENT_ID);

    try {
        const {
            data
        } = await Axios.post("https://accounts.spotify.com/api/token", params);

        log("refresh", "Success!");
        return res.send(data);
    } catch (ex) {
        log("error", ex);
    }
});


https.createServer({
    key: fs.readFileSync("./key.pem"),
    cert: fs.readFileSync("./cert.pem"),
    passphrase: PASS_PHRASE
}, app).listen(PORT);
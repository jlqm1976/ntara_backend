import { useState } from "react";
import { Link } from "react-router-dom";
import {
    Box,
    Button,
    Typography,
    Paper,
    Stack,
    Alert,
} from "@mui/material";

export default function UploadPage() {
    const [file, setFile] = useState(null);
    const [status, setStatus] = useState("");

    const handleUpload = async (e) => {
        e.preventDefault();
        if (!file) {
            setStatus("⚠️ Please select a CSV file first.");
            return;
        }

        const formData = new FormData();
        formData.append("file", file);

        try {
            const res = await fetch("http://localhost:5269/TeamScore/UploadCsv", {
                method: "POST",
                body: formData,
            });

            if (res.ok) {
                setStatus("✅ CSV file uploaded successfully.");
                setFile(null); // Clear the selected file
            } else {
                setStatus("❌ Error uploading the file.");
            }
        } catch (err) {
            console.error(err);
            setStatus("❌ Server connection error.");
        }
    };

    return (
        <Box
            sx={{
                minHeight: "100vh",
                display: "flex",
                alignItems: "center",
                justifyContent: "center",
                bgcolor: "#f5f5f5",
                p: 2,
            }}
        >
            <Paper elevation={3} sx={{ p: 4, maxWidth: 500, width: "100%" }}>
                <Typography variant="h5" fontWeight="bold" gutterBottom>
                    Upload CSV File
                </Typography>
                <Typography variant="body2" color="text.secondary" gutterBottom>
                    Select a CSV file and upload it to register the team data.
                </Typography>

                <Stack
                    component="form"
                    spacing={2}
                    onSubmit={handleUpload}
                    sx={{ mt: 2 }}
                >
                    <Button variant="outlined" component="label">
                        Select File
                        <input
                            type="file"
                            hidden
                            accept=".csv"
                            onChange={(e) => setFile(e.target.files[0])}
                        />
                    </Button>
                    {file && (
                        <Typography variant="body2" color="text.primary">
                            Selected file: <b>{file.name}</b>
                        </Typography>
                    )}

                    <Stack direction="row" spacing={2}>
                        <Button
                            type="submit"
                            variant="contained"
                            color="primary"
                            disabled={!file}
                        >
                            Upload CSV
                        </Button>

                        {/* Button always visible */}
                        <Button
                            component={Link}
                            to="/teams"
                            variant="outlined"
                            color="secondary"
                        >
                            View Data
                        </Button>
                    </Stack>
                </Stack>

                {status && (
                    <Alert
                        severity={
                            status.includes("✅")
                                ? "success"
                                : status.includes("⚠️")
                                    ? "warning"
                                    : "error"
                        }
                        sx={{ mt: 3 }}
                    >
                        {status}
                    </Alert>
                )}
            </Paper>
        </Box>
    );
}

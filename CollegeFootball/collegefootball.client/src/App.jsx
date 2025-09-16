import { BrowserRouter as Router, Routes, Route } from "react-router-dom";
import UploadPage from "./pages/UploadPage";
import TeamScorePage from "./pages/TeamScorePage";

export default function App() {
    return (
        <Router>
            <Routes>
                <Route path="/" element={<UploadPage />} />
                <Route path="/teams" element={<TeamScorePage />} />
            </Routes>
        </Router>
    );
}
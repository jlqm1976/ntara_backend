// DataPage.jsx
import React, { useEffect, useState } from "react";
import {
    Box,
    Button,
    CircularProgress,
    FormControl,
    InputLabel,
    MenuItem,
    OutlinedInput,
    Select,
    Checkbox,
    ListItemText,
    TextField,
    Typography,
    Paper,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
} from "@mui/material";

const DataPage = () => {
    const [data, setData] = useState([]);
    const [columns, setColumns] = useState([]);
    const [selectedColumns, setSelectedColumns] = useState([]);
    const [searchValue, setSearchValue] = useState("");
    const [loading, setLoading] = useState(false);

    // Load all data on first render
    useEffect(() => {
        fetchAllData();
        fetchColumns();
    }, []);

    const fetchAllData = async () => {
        setLoading(true);
        try {
            const response = await fetch("http://localhost:5269/TeamScore/GetAll");
            const result = await response.json();
            setData(result);
        } catch (error) {
            console.error("Error fetching all data:", error);
        } finally {
            setLoading(false);
        }
    };

    const fetchColumns = async () => {
        try {
            const response = await fetch("http://localhost:5269/SearchableColumn/GetAll");
            const result = await response.json();
            setColumns(result);
        } catch (error) {
            console.error("Error fetching columns:", error);
        }
    };

    const handleSearch = async () => {
        if (!searchValue || selectedColumns.length === 0) return;

        const query = new URLSearchParams();
        query.append("searchValue", searchValue);
        selectedColumns.forEach((col) => query.append("columns", col));

        setLoading(true);
        try {
            const response = await fetch(`http://localhost:5269/TeamScore/Search?${query.toString()}`);
            const result = await response.json();
            setData(result);
        } catch (error) {
            console.error("Error searching data:", error);
        } finally {
            setLoading(false);
        }
    };

    return (
        <Box p={4}>
            <Typography variant="h4" gutterBottom>
                Team Scores
            </Typography>

            {/* Search Controls */}
            <Box display="flex" gap={2} mb={3}>
                <TextField
                    label="Search value"
                    variant="outlined"
                    value={searchValue}
                    onChange={(e) => setSearchValue(e.target.value)}
                />

                <FormControl sx={{ minWidth: 250 }}>
                    <InputLabel id="columns-label">Select columns</InputLabel>
                    <Select
                        labelId="columns-label"
                        multiple
                        value={selectedColumns}
                        onChange={(e) => setSelectedColumns(e.target.value)}
                        input={<OutlinedInput label="Select columns" />}
                        renderValue={(selected) =>
                            selected
                                .map((col) => {
                                    const column = columns.find((c) => c.columnName === col);
                                    return column ? column.displayName : col;
                                })
                                .join(", ")
                        }
                    >
                        {columns.map((col) => (
                            <MenuItem key={col.id} value={col.columnName}>
                                <Checkbox checked={selectedColumns.indexOf(col.columnName) > -1} />
                                <ListItemText primary={col.displayName} />
                            </MenuItem>
                        ))}
                    </Select>
                </FormControl>

                <Button variant="contained" color="primary" onClick={handleSearch}>
                    Search
                </Button>
                <Button variant="outlined" color="secondary" onClick={fetchAllData}>
                    Load All
                </Button>
            </Box>

            {/* Table */}
            {loading ? (
                <CircularProgress />
            ) : (
                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>Id</TableCell>
                                <TableCell>Rank</TableCell>
                                <TableCell>Team Name</TableCell>
                                <TableCell>Mascot Name</TableCell>
                                <TableCell>Last Win Date</TableCell>
                                <TableCell>Winning %</TableCell>
                                <TableCell>Total Wins</TableCell>
                                <TableCell>Total Losses</TableCell>
                                <TableCell>Total Ties</TableCell>
                                <TableCell>Total Games</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {data.map((row) => (
                                <TableRow key={row.id}>
                                    <TableCell>{row.id}</TableCell>
                                    <TableCell>{row.rank}</TableCell>
                                    <TableCell>{row.teamName}</TableCell>
                                    <TableCell>{row.mascotName}</TableCell>
                                    <TableCell>{row.lastWinDate}</TableCell>
                                    <TableCell>{row.winningPercentage}</TableCell>
                                    <TableCell>{row.totalWins}</TableCell>
                                    <TableCell>{row.totalLosses}</TableCell>
                                    <TableCell>{row.totalTies}</TableCell>
                                    <TableCell>{row.totalGames}</TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}
        </Box>
    );
};

export default DataPage;

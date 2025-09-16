// DataPage.jsx
import React, { useEffect, useState } from "react";
import {
    Alert,
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
    Paper,
    Snackbar,
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TablePagination,
    TableRow,
    TextField,
    Typography
} from "@mui/material";

const DataPage = () => {
    const [data, setData] = useState([]);
    const [columns, setColumns] = useState([]);
    const [selectedColumns, setSelectedColumns] = useState([]);
    const [searchValue, setSearchValue] = useState("");
    const [loading, setLoading] = useState(false);
    const [page, setPage] = useState(0);
    const [rowsPerPage, setRowsPerPage] = useState(25);
    const [openAlert, setOpenAlert] = useState(false);

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

            // Select all columns by default
            setSelectedColumns(result.map((col) => col.columnName));

        } catch (error) {
            console.error("Error fetching columns:", error);
        }
    };

    const handleSearch = async () => {
        if (!searchValue || selectedColumns.length === 0) {
            setOpenAlert(true);
            return;
        }

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

                <FormControl sx={{ minWidth: 250, maxWidth: 300 }}>
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
                    <TablePagination
                        component="div"
                        count={data.length}
                        page={page}
                        onPageChange={(event, newPage) => setPage(newPage)}
                        rowsPerPage={rowsPerPage}
                        onRowsPerPageChange={(event) => {
                            setRowsPerPage(parseInt(event.target.value, 10));
                            setPage(0);
                        }}
                        rowsPerPageOptions={[25, 50, 100]}
                    />
                    <Table>
                        <TableHead>
                                <TableRow sx={{ backgroundColor: "#219ebc", "& th": { fontWeight: "bold" } }}>
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
                                {data
                                    .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
                                    .map((row) => (
                                <TableRow key={row.id} hover>
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
            <Snackbar
                open={openAlert}
                autoHideDuration={3000}
                onClose={() => setOpenAlert(false)}
                anchorOrigin={{ vertical: "top", horizontal: "center" }}
            >
                <Alert
                    onClose={() => setOpenAlert(false)}
                    severity="warning"
                    sx={{ width: "100%" }}
                >
                    Search value cannot be empty and at least one column must be selected!
                </Alert>
            </Snackbar>
        </Box>
    );
};

export default DataPage;

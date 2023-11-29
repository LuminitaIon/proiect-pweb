import React, { useState } from 'react';
import Navbar from '../../components/index';
import logopw from '../../assets/logopw.png';
import './style.css';
import Avatar from '@mui/material/Avatar';
import Button from '@mui/material/Button';
import CssBaseline from '@mui/material/CssBaseline';
import TextField from '@mui/material/TextField';
import FormControlLabel from '@mui/material/FormControlLabel';
import Checkbox from '@mui/material/Checkbox';
import Link from '@mui/material/Link';
import Paper from '@mui/material/Paper';
import Box from '@mui/material/Box';
import Grid from '@mui/material/Grid';
import LockOutlinedIcon from '@mui/icons-material/LockOutlined';
import Typography from '@mui/material/Typography';


class  Login extends React.Component {
    handleSubmit (event) {
        event.preventDefault();
        const data = new FormData(event.currentTarget);
        console.log({
            email: data.get('email'),
            password: data.get('password'),
        });
    };
    render() {
    return (
            <div>
                <Navbar /> 
                <div className="login-page">
                    <div className="login-form">
                        <div className="form-container">
                            <div>
                                <Grid container component="main" 
                                    sx={{ height: '50%', width: 'auto', 
                                    display: 'flex',
                                    flexDirection: 'row',
                                    justifyContent: 'space-around',
                                    alignContent: 'column'
                                    }}>
                                    <CssBaseline />
                                    <Grid
                                    item
                                    xs={false}
                                    sm={4}
                                    md={7}
                                    sx={{
                                        backgroundImage: `url(${logopw})`,
                                        backgroundRepeat: 'no-repeat',
                                        backgroundSize: '70%',
                                        backgroundPosition: 'center',
                                    }}
                                    />
                                    <Grid item xs={10} sm={7} md={5} component={Paper} elevation={8} square padding={5}>
                                    <Box
                                        sx={{
                                        
                                        display: 'flex',
                                        flexDirection: 'column',
                                        justifyContent: 'space-between',
                                        alignItems: 'center',
                                        
                                        }}
                                    >
                                        
                                        <Typography component="h1" variant="h5" 
                                        sx={{
                                            fontFamily: 'Roboto',
                                            fontSize:'3em',
                                            fontWeight: 'light',
                                            position: 'relative',}}>
                                        Welcome!
                                        </Typography>
                                        <Box component="form" noValidate sx={{ mt: 1 }}>
                                        <label>Username</label>
                                        <TextField
                                            margin="normal"
                                            required
                                            fullWidth
                                            id="email"
                                            label="Enter your username"
                                            name="username"
                                            autoComplete="name"
                                            autoFocus
                                        />
                                        <label>Password</label>
                                            <TextField
                                                margin="normal"
                                                required
                                                fullWidth
                                                name="password"
                                                label="Enter your password"
                                                type="password"
                                                id="password"
                                                autoComplete="current-password"
                                            />
                                        <Box
                                            sx={{
                                                display: 'flex',
                                                flexDirection: 'row',
                                                justifyContent: 'flex-start',
                                                }}
                                            >
                                            <FormControlLabel
                                                control={<Checkbox value="remember" color="primary" />}
                                                label="Remember me"
                            
                                                
                                            />
                                            <Grid container>
                                                <Grid item xs>
                                                <Link href="#" variant="body2">
                                                    Forgot password?
                                                </Link>
                                                </Grid>
                                            </Grid>
                                        </Box>
                                        <Button
                                            type="submit"
                                            fullWidth
                                            variant="contained"
                                            
                                            sx={{ mt: 3, mb: 2, backgroundColor:'#2B0245'}}
                                        >
                                            Login
                                        </Button>
                                        </Box>
                                        <Grid item sx={{
                                        display: 'flex',
                                        flexDirection: 'row',
                                        }}>
                                                <div>
                                                Don't have an account?
                                                </div>
                                                <Link href="#" variant="body2" color="#2B0245">
                                                    REGISTER
                                                </Link>
                                        </Grid>
                                    </Box>
                                    </Grid>
                                </Grid>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        )
    }
    
}

export default Login;
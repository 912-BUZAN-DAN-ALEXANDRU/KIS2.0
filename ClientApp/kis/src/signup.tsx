import {
    makeStyles,
    Container,
    Typography,
    TextField,
    Button,
} from "@material-ui/core";
import { useState } from "react";
import { useForm } from "react-hook-form";
import * as yup from "yup";
import { yupResolver } from "@hookform/resolvers/yup";
import * as React from 'react';
import * as ReactDOM from 'react-dom';


interface IFormInput {
    email: string;
    userName: string;
    password: string;
    confirmPassword: string;
}

const schema = yup.object().shape({
    email: yup.string().required().email(),
    userName: yup.string().required().min(2).max(25),
    password: yup.string().required().min(8).max(120),
    confirmPassword: yup.string().required().min(8).max(120).oneOf([yup.ref('password'), null], 'Passwords must match'),
});

const useStyles = makeStyles((theme) => ({
    heading: {
        textAlign: "center",
        margin: theme.spacing(1, 0, 4),
    },
    submitButton: {
        marginTop: theme.spacing(4),
    },
}));

function SignUp() {

    const {
        register,
        handleSubmit,
        formState: { errors },
    } = useForm<IFormInput>({
        resolver: yupResolver(schema),
    });

    const { heading, submitButton } = useStyles();

    const [res, setResponse] = useState<string>();

    const onSubmit = (data: IFormInput) => {
        let userInfo = {
            Email: data.email,
            Password: data.password,
            Name: data.userName
        };

        fetch('https://localhost:44324/Register', {
            method: 'POST',
            headers: { 'Content-type': 'application/json' },
            body: JSON.stringify(userInfo)
        }).then(response => {
            console.log(response);
            if (!response.ok) {
                if (response.status == 409)
                    throw new Error("User already exists!");
                else throw new Error("Request failed error " + response.statusText);
            }
            const data = response.json();

            setResponse("User has been created!");
        }).catch(error => {
            console.log(error.message);
            setResponse(error.message);
        });
    };

    return (
        <Container maxWidth="xs">
            <Typography className={heading} variant="h3">
                Sign Up Form
            </Typography>
            <form onSubmit={handleSubmit(onSubmit)} noValidate>
                <TextField
                    {...register("email")}
                    variant="outlined"
                    margin="normal"
                    label="Email"
                  //  helperText={errors.email?.message}
                   // error={!!errors.email?.message}
                    fullWidth
                    required
                />
                <TextField
                    {...register("userName")}
                    variant="outlined"
                    margin="normal"
                    label="Username"

                    fullWidth
                    required
                />
                <TextField
                    {...register("password")}
                    variant="outlined"
                    margin="normal"
                    label="Password"
                    type="password"

                    fullWidth
                    required
                />

                <TextField
                    {...register("confirmPassword")}
                    variant="outlined"
                    margin="normal"
                    label="Confirm Password"
                    type="password"

                    fullWidth
                    required
                />
                <Button
                    type="submit"
                    fullWidth
                    variant="contained"
                    color="primary"
                    className={submitButton}
                >
                    Sign Up
                </Button>
                {res && (
                    <>
                        <Typography variant="body2">{res}</Typography>
                    </>
                )}
            </form>
        </Container>
    );
}

export default SignUp;

import React, { useState, useEffect } from 'react';
import auth from '@react-native-firebase/auth';
import { Button, TextInput, View, Text } from 'react-native';
// import firestore from '@react-native-firebase/firestore';

// const users = await firestore().collection('Users').get();
// const user = await firestore().collection('Users').doc('ABC').get();



const HomeScreen = (props) => {

    let fireBase = JSON.parse(props.route.params.fbaseData);
    console.log('home props', JSON.parse(props.route.params.fbaseData))
    const logOut = () => auth()
        .signOut()
        .then(() => console.log('User signed out!'));

    const getUserData = async () => {
        try {
            const response = await fetch('');
            const json = await response.json();
            // setData(json.movies);
        } catch (error) {
            console.error(error);
        } finally {
            // setLoading(false);
        }
    }

    useEffect(() => {
        getUserData();
    }, []);

    // useEffect(() => {
    //     const asyncCall = async () => {
    //         try {

    //         } catch (e) {
    //             console.log('invoice err', e);
    //         }
    //     };
    //     asyncCall();

    // }, []);

    return (
        <View style={{ flex: 1, alignItems: 'center', justifyContent: 'center' }}>
            <Text style={{ fontSize: 18, color: 'black', fontWeight: 'bold' }}>Home Screen</Text>
            <Text style={{ marginVertical: 10, fontSize: 20, color: 'green', fontWeight: 'bold' }}>Welcome {fireBase.phoneNumber}</Text>
            <Button title="Log Out" onPress={() => logOut()} />
        </View>
    );
}

export default HomeScreen;
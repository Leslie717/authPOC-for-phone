import React, { useState, useEffect } from 'react';
import { Button, TextInput, Text, View, TouchableOpacity } from 'react-native';
import auth from '@react-native-firebase/auth';
import InputBasic from '../components/commonTextField'
import firestore from '@react-native-firebase/firestore';


const LogIn = () => {
    // If null, no SMS has been sent
    const [confirm, setConfirm] = useState(null);

    const [code, setCode] = useState('');
    const [number, setNumber] = useState('');
    const [firstName, setFirstName] = useState('');
    const [lastName, setLastName] = useState('');

    // Handle the button press
    async function signInWithPhoneNumber(phoneNumber) {
        console.log('phoneNumber', `+91 ${phoneNumber}`)
        let number = `+91 ${phoneNumber}`;
        const confirmation = await auth().signInWithPhoneNumber(number);
        // console.log('confirmation', confirmation)
        setConfirm(confirmation);
    }

    async function confirmCode() {
        try {
            await confirm.confirm(code).then((res) => {
                // console.log('confirmation res ', res);
                console.log('confirmation uid', res.user._user.uid);
                //POST
                if (res.additionalUserInfo.isNewUser) {
                    firestore()
                        .collection('phoneAuthData')
                        .doc(res.user._user.uid)
                        .set({
                            firstName: firstName,
                            lastName: lastName,
                            phoneNumber: number
                        })
                        // .then(() => {
                        //     console.log('loggin in new');
                        //     login();
                        // })
                        .then(() => {
                            console.log('User added!');
                            // alert('User added!');
                        }).catch((err) => { console.log('add err', err); });
                }
                else {
                    firestore()
                        .collection('phoneAuthData')
                        .doc(res.user._user.uid)
                        .update({
                            firstName: firstName,
                            lastName: lastName,
                            phoneNumber: number
                        })
                        // .then(() => {
                        //     console.log('loggin in update');
                        //     login();
                        // })
                        .then(() => {
                            console.log('User updated!');
                        }).catch((err) => { console.log('update err', err); });
                }
            }); // const res = await 
        } catch (error) {
            alert('Invalid code.');
            console.log('Invalid code.', error);
        }
    }



    const login = (req, res) => {
        console.log("calling")
        let loginData = {
            userName: "string",
            password: "string"
        }
        // const request = new Request('http://192.168.0.113/api/auth/authenticate', { method: 'POST', body: '{"userName": "string","password": "string"}' });         fetch(request)


        fetch('http://192.168.0.113/api/Auth/authenticate', {
            //https://localhost:7273/swagger/index.html, http://localhost:9501/api/Auth/authenticate, http://192.168.0.113:9501/api/Auth/authenticate
            method: 'POST',
            credentials: 'include',
            headers: {
                // 'Accept': 'application/json',
                'Accept': "*/*",
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(loginData)
            // body: loginData
        }).then((response) => {
            console.log("then")
            response.json()
        }).then((json) => {
            console.log("fetch response", json)
            setData(json)
        }).catch((error) => console.log('fetch error', error))
        // .finally(() => setLoading(false));
    }


    const confirmNLogin = () => {
        confirmCode().then(async () => {
            console.log('loggin in');
            await login();
        }).catch((error) => console.error('confirmNLogin error', error.message))
    }


    useEffect(() => {
        // const asyncCall = async () => {
        //     try {

        //     } catch (e) {
        //         console.log('invoice err', e);
        //     }
        // };
        // asyncCall();


        if (confirm != null) {
            console.log('confirm', confirm);
        }


    }, [confirm]);


    // if (!confirm) {
    return (
        <View style={{
            margin: 5,
            justifyContent: 'center',
            alignItems: 'center',
            flex: 1

        }}>
            <Text style={{
                textAlign: 'center',
                fontSize: 28,
                fontWeight: 'bold',
                lineHeight: 28
            }}>
                Welcome
            </Text>

            <InputBasic
                label='First Name'
                placeholder="Enter your first name"
                inputVal={firstName}
                setInputVal={val => setFirstName(val)}
                type='default'
            />
            <InputBasic
                label='Last Name'
                placeholder="Enter your last name"
                inputVal={lastName}
                setInputVal={val => setLastName(val)}
                type='default'
            />
            <InputBasic
                label='Number'
                placeholder="Please enter your number here"
                inputVal={number}
                setInputVal={val => setNumber(val)}
                type='phone-pad'
            />


            <TouchableOpacity
                style={{ width: '75%', padding: 5, backgroundColor: 'navy', borderRadius: 10, marginVertical: 10 }}
                // title="Sign In"
                // onPress={() => signInWithPhoneNumber(number)}
                onPress={() => login()}

            >
                <Text style={{
                    textAlign: 'center',
                    color: 'white',
                    fontWeight: 'bold',
                }}>
                    Sign In
                </Text>
            </TouchableOpacity>
        </View>
    );
    // }

    // return (
    //     <View style={{
    //         margin: 5,
    //         justifyContent: 'center',
    //         alignItems: 'center',
    //         flex: 1

    //     }}>
    //         <TextInput
    //             style={{
    //                 width: '80%',
    //                 backgroundColor: 'white',
    //                 borderColor: 'black',
    //                 borderWidth: 1,
    //                 borderRadius: 10,
    //                 marginVertical: 5
    //             }}
    //             placeholder="Enter code"
    //             value={code} onChangeText={text => setCode(text)} />
    //         <TouchableOpacity
    //             style={{ width: '75%', padding: 5, backgroundColor: 'yellow', borderRadius: 10, marginVertical: 10 }}
    //             onPress={() => login()}
    //         // onPress={() => confirmNLogin()}

    //         >
    //             <Text style={{
    //                 textAlign: 'center',
    //                 color: 'black',
    //                 fontWeight: 'bold',
    //             }}>
    //                 Confirm
    //             </Text>
    //         </TouchableOpacity>
    //     </View>
    // );
}

export default LogIn;
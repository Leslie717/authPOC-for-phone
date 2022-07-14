// import * as React from 'react';
import React, { useState, useEffect } from 'react';
import { View, Text } from 'react-native';
import auth from '@react-native-firebase/auth';
import { NavigationContainer } from '@react-navigation/native';
import { createNativeStackNavigator } from '@react-navigation/native-stack';
import LogIn from './src/screens/login';
import HomeScreen from './src/screens/home'
import firestore from '@react-native-firebase/firestore';


function DetailsScreen() {
  return (
    <View style={{ flex: 1, alignItems: 'center', justifyContent: 'center' }}>
      <Text>Initialising...</Text>
    </View>
  );
}

const Stack = createNativeStackNavigator();

function App() {

  // Set an initializing state whilst Firebase connects
  const [initializing, setInitializing] = useState(true);
  const [user, setUser] = useState();

  // Handle user state changes
  function onAuthStateChanged(user) {
    setUser(user);
    if (user != null) {
      console.log('uid OASC user._user.uid', user._user.uid);
      // console.log('user._user.providerData[0].phoneNumber', user._user.providerData[0].phoneNumber);

      //GET
      // firestore()
      //   .collection('phoneAuthData')
      //   // Filter results
      //   .where('phoneNumber', '==', user._user.providerData[0].phoneNumber)
      //   .get()
      //   .then(querySnapshot => {
      //     // console.log('Total users: ', querySnapshot.size);
      //     querySnapshot.forEach((dataSnapshot) => {
      //       console.log('query data', dataSnapshot);
      //     })
      //     /* ... */
      //   });

      //POST
      // firestore()
      //   .collection('phoneAuthData')
      //   .add({
      //     firstName: 'Ada',
      //     lastName: 'Lovelace',
      //     phoneNumber: user._user.providerData[0].phoneNumber
      //   })
      //   .then(() => {
      //     console.log('User added!');
      //   });

    }
    if (initializing) setInitializing(false);
  }

  useEffect(() => {
    const subscriber = auth().onAuthStateChanged(onAuthStateChanged);
    return subscriber; // unsubscribe on unmount
  }, []);

  if (initializing) return <DetailsScreen />;



  return (
    <NavigationContainer>
      <Stack.Navigator initialRouteName="Login">
        {
          !user
            ? <Stack.Screen
              name="Login"
              component={LogIn}
            // options={{ title: 'Welcome' }}
            // options={{ headerShown: false }}
            />
            :
            // <View>
            <Stack.Screen name="Home" component={HomeScreen} initialParams={{ fbaseData: JSON.stringify(user) }} />
          //   {/* <Stack.Screen name="Details" component={DetailsScreen} />
          // </View> */}
        }
      </Stack.Navigator>
    </NavigationContainer>
  );
}

export default App;
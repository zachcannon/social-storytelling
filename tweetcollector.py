import tweepy
import json
import math

class TweetCollector():
    def __init__(self):
        
        # SS Authorization information
        self.consumer_key = 'i6QLDidk5RyUVJAKammGlQ'
        self.consumer_secret = 'wn0u2e0RJ0bdTpyiqGDVmYJvracDHaRPiOQxclZZ8VI'
        self.access_token_key = '2372951286-NrmzZz0wUB4ZzPF8wGMPEaAWnCa4YcMAjQ93CDo'
        self.access_token_secret = 'Rmx7dnqkP4MyoXm3A1zj1hPwJSz44dH2AnWzOvigSpELf'
        
        self.auth = tweepy.auth.OAuthHandler(self.consumer_key, self.consumer_secret)
        self.auth.set_access_token(self.access_token_key, self.access_token_secret)
        
        # data storage
        self.queries = "@socialstoriez"
        self.results = {}
    
    def collectOthersTweets(self):
        # collects and creates a file of tweet about socialstoriez
        
        api = tweepy.API(self.auth)
        
        # go through queries mentioning our twitter
        with open('TweetsToUs', 'a+') as outfile:
            for tweet in tweepy.Cursor(api.search,q= self.queries, lang = "en").items(100):
                self.results[tweet.id] = {}
                self.results[tweet.id]['Screen Name'] = tweet.user.screen_name
                self.results[tweet.id]['Name'] = tweet.user.name
                self.results[tweet.id]['tId'] = tweet.id
                self.results[tweet.id]['Text']= tweet.text
                self.results[tweet.id]['Retweets'] = tweet.retweet_count
                #self.results[tweet.id]['Favorites'] = tweet.favoriters_count
                self.results[tweet.id]['Created'] = str(tweet.created_at)
                self.results[tweet.id]['Entities']= tweet.entities
                self.results[tweet.id]['Description'] = tweet.user.description
                self.results[tweet.id]['Profile Image'] = tweet.user.profile_image_url
                self.results[tweet.id]['Text'] = tweet.text.lower().encode('utf-8')
                
                json.dump(self.results[tweet.id],outfile)
                outfile.write('\n')
        print len(self.results.keys())
                                    
    def read_data(self,filename):
        #Used to read all tweets from the json file
        data = []
        try:
            with open(filename) as f:
                for line in f:
                    data.append(json.loads(line.strip()))
        except:
            print "Failed to read data!"
            return []
        print "The json file has been successfully read!"
        
        for tweet in data:
            self.results[tweet['tId']]= tweet
        #print len(self.results.keys())


if __name__ == "__main__":
    tc = TweetCollector()
    #tc.readInput()
    tc.collectOthersTweets()
        
    #tc.read_data('ResultsTOR')

    #tc.read_stars('StarsTOR')
    #print "ranking yo"
    #tc.outputdata()

using PolySondage.Data.Models;
using PolySondage.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace PolySondage.Services
{
    public class TestDataBase
    {
        private readonly IPollRepository _pollRepo;
        private readonly IUserRepository _userRepo;
        private readonly IVoteRepository _voteRepo;
        private int user1;
        private int user2;
        private int user3;
        private int poll1;
        private int poll2;
        private int poll3;


        public TestDataBase(IPollRepository pollrepo, IUserRepository userrepo, IVoteRepository voterepo)
        {
            _pollRepo = pollrepo;
            _userRepo = userrepo;
            _voteRepo = voterepo;
        }

        public async Task test()
        {
            createdBaseExemple().Wait();
            Debug.WriteLine("Base créer");
            await testUser();
            Debug.WriteLine("Fin erreur Poll");
            await testPoll();
            Debug.WriteLine("Fin erreur User");
            await testVote();
            Debug.WriteLine("Fin erreur Vote");
        }

        private async Task createdBaseExemple()
        {
            User u1 = new User();
            u1.Email = "user1@mail.fr";
            u1.Password = "123";
            User u2 = new User();
            u2.Email = "user2@mail.fr";
            u2.Password = "456";
            User u3 = new User();
            u3.Email = "user3@mail.fr";
            u3.Password = "789";

            int id1 = await _userRepo.AddUserAsync(u1);
            int id2 = await _userRepo.AddUserAsync(u2);
            int id3 = await _userRepo.AddUserAsync(u3);
            Debug.WriteLine("id user 1 " + id1 + "/n id user 2 " + id2 + "/n id user 3 " + id3);
            user1 = id1;
            user2 = id2;
            user3 = id3;

            List<Choice> c = new List<Choice>();
            Choice tmp = new Choice();
            tmp.Details = "c1";
            c.Add(tmp);
            tmp.Details = "c2";
            c.Add(tmp);
            tmp.Details = "c3";
            c.Add(tmp);

            Poll p = new Poll();
            p.IdUser = id1;
            p.Unique = true;
            p.Choices = c;
            p.Title = "S1";
            int s1 = await _pollRepo.AddPollAsync(p);

            p.Unique = false;
            p.Title = "S2";
            int s2 = await _pollRepo.AddPollAsync(p);

            p.IdUser = id2;
            p.Title = "S3";
            int s3 = await _pollRepo.AddPollAsync(p);

            Debug.WriteLine("Sondage 1 " + s1 + " Sondage 2 " + s2 + " Sondage 3 " + s3);
            poll1 = s1;
            poll2 = s2;
            poll3 = s3;


            Vote v = new Vote();

            v.IdUser = id1;
            v.IdPoll = s1;
            v.Choices.Add(c[0]);
            await _voteRepo.AddVoteAsync(v);

            v.IdUser = id3;
            await _voteRepo.AddVoteAsync(v);

            v.IdUser = id2;
            v.Choices.RemoveAt(0);
            v.Choices.Add(c[1]);
            await _voteRepo.AddVoteAsync(v);

            v.IdPoll = s2;
            v.Choices.Add(c[0]);
            v.Choices.Add(c[2]);
            await _voteRepo.AddVoteAsync(v);

            v.Choices.RemoveAt(2);
            v.Choices.RemoveAt(1);
            v.IdUser = id1;
            await _voteRepo.AddVoteAsync(v);

            v.IdPoll = s3;
            await _voteRepo.AddVoteAsync(v);

            v.IdUser = id2;
            v.Choices.Add(c[1]);
            v.Choices.Add(c[2]);
            await _voteRepo.AddVoteAsync(v);

            v.IdUser = id3;
            v.Choices.Add(c[0]);
            await _voteRepo.AddVoteAsync(v);
        }

        private async Task testUser()
        {
            User u1 = await _userRepo.getUserByIdAsync(user1);
            if (u1.Email != "user1@mail.fr" && u1.Password != "123")
                Debug.WriteLine("information de user1 non conforme");

            if (u1.Created.Count != 2)
                Debug.WriteLine("Nombre de sondage incorrect, renvoie :" + u1.Created.Count);

            User u2 = await _userRepo.getUserByIdAsync(user2);
            if (u2.Email != "user2@mail.fr" && u2.Password != "456")
                Debug.WriteLine("information de user2 non conforme");

            if (u2.Created.Count != 1)
                Debug.WriteLine("Nombre de sondage incorrect, renvoie :" + u2.Created.Count);

            User u3 = await _userRepo.getUserByIdAsync(user1);
            if (u3.Email != "user3@mail.fr" && u3.Password != "789")
                Debug.WriteLine("information de user3 non conforme");

            if (u3.Created.Count != 0)
                Debug.WriteLine("Nombre de sondage incorrect, renvoie :" + u3.Created.Count);

            if (await _userRepo.connectUserAsync("user1@mail.fr", "123") != user1)
                Debug.WriteLine("Bon info de connexion mais mauvais retour");

            if (await _userRepo.connectUserAsync("user1@mail.fr", "124") != -1)
                Debug.WriteLine("Mauvaise mot de passe mais connexion accepté");

            if (await _userRepo.connectUserAsync("r1@mail.fr", "123") != -1)
                Debug.WriteLine("Mauvaise email mais connexion accepté");
        }

        private async Task testPoll()
        {
            Poll p2 = await _pollRepo.GetPollByIdAsync(poll2);
            if (p2.IdUser != user1 && p2.Choices[0].Details != "1")
                Debug.WriteLine("recherche par id renvoie pas le bon poll, id = " + p2.IdPoll + " et id user : " + p2.IdUser);

            if (!await _pollRepo.IsPollActivateAsync(poll3))
                Debug.WriteLine("le poll 3 doit être activé mais la fonction ne revoie pas cette information");
            await _pollRepo.DeactivatePollAsync(poll3);
            if (await _pollRepo.IsPollActivateAsync(poll3))
                Debug.WriteLine("le poll 3 doit être désactivé mais la fonction ne revoie pas cette information");

            if (await _pollRepo.GetNumberUserVotePollAsync(poll1) != 3)
                Debug.WriteLine("Mauvais nombre de user qui ont voté pour poll3");

            if (await _pollRepo.GetNumberUserVotePollAsync(poll2) != 2)
                Debug.WriteLine("Mauvais nombre de user qui ont voté pour poll2");
        }

        private async Task testVote()
        {
            Poll p = await _pollRepo.GetPollByIdAsync(poll3);
            Vote v = new Vote();
            v.IdPoll = poll3;
            v.IdUser = user3;
            v.Choices.Add(p.Choices[2]);
            v.Choices.Add(p.Choices[1]);

            int nbvote1 = p.Choices[0].Vote;
            await _voteRepo.ChangeVoteAsync(v);
            Poll pchange = await _pollRepo.GetPollByIdAsync(poll3);
            if (!(nbvote1 == pchange.Choices[0].Vote + 1))
                Debug.WriteLine("le vote n'a pas été changé");
            if (pchange.Choices[0].Vote == p.Choices[0].Vote)
                Debug.WriteLine("mise à jour automatique du poll après changement de vote");

            List<Poll> plist = await _voteRepo.GetPaticipatedPollsByIdUserAsync(user1);
            if (plist.Count != 3)
                Debug.WriteLine("mauvaise lsite de participation d'un user");
        }
    }
}
